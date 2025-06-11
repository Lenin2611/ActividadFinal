internal class Program
{
    private static readonly string[] defaultUser = ["admin", "1234"];

    static string[,] users = new string[5, 15];
    static int usersCount = 0;

    static string[,] items = new string[4, 15];
    static int itemsCount = 0;

    static string[,] soldItems = new string[5, 10];
    static int soldItemsCount = 0;


    private static void Main(string[] args)
    {
        Auth();
    }

    private static void Auth()
    {
        Console.Clear();
        bool loop = true;
        while (loop)
        {
            Console.Write("Ingrese el nombre de usuario: ");
            string inputUser = Console.ReadLine()!;

            Console.Write("Ingrese la contraseña: ");
            string inputPassword = Console.ReadLine()!;

            Console.Clear();
            if (inputUser == defaultUser[0] && inputPassword == defaultUser[1])
            {
                loop = false;
                ShowMessage("¡Bienvenido al sistema!", true);
                ShowMainMenu();
            }
            else
            {
                ShowMessage("Error de autenticación. Usuario o contraseña incorrectos.", false);
            }
        }
    }

    private static void ShowMainMenu()
    {
        bool loop = true;
        while (loop)
        {
            switch (ShowMenu("main"))
            {
                case "1":
                    ShowMessage("¡Bienvenido al módulo de Gestión de usuarios!", true);
                    Users();
                    break;
                case "2":
                    ShowMessage("¡Bienvenido al módulo de Gestión de artículos!", true);
                    Items();
                    break;
                case "3":
                    ShowMessage("¡Bienvenido al módulo de Gestión de ventas!", true);
                    Sales();
                    break;
                case "4":
                    Console.WriteLine("Se ha cerrado sesión en el programa. ¡Hasta luego!\n");
                    loop = false;
                    break;
                default:
                    ShowMessage("Ingrese una opción del menú válida.", false);
                    break;
            }
        }
    }

    private static void Users()
    {
        bool pass = true;
        while (pass)
        {
            Console.Clear();
            switch (ShowMenu("usuarios"))
            {
                case "1":
                    ShowUser();
                    break;
                case "2":
                    AddUser();
                    break;
                case "3":
                    EditUser();
                    break;
                case "4":
                    pass = false;
                    break;
                default:
                    ShowMessage("Opción no válida.", true);
                    break;
            }
        }
    }

    private static void ShowUser()
    {
        if (usersCount == 0)
        {
            ShowMessage("No hay usuarios registrados.", false);
            return;
        }
        else
        {    
            Console.WriteLine("Lista de usuarios:");
            for (int i = 0; i < usersCount; i++)
            {
                Console.WriteLine($"{i + 1}. {GetUserId(i)} - {GetUserName(i)} {GetUserLastname(i)} - Tel: {GetUserPhone(i)} - Dir: {GetUserAddress(i)}");
            }
            ShowMessage("", false);
        }
    }

    private static void AddUser()
    {
        bool loop = true;
        while (loop)
        {
            if (usersCount == 15)
            {
                ShowMessage("No se pueden agregar más usuarios. Límite alcanzado.", false);
                return;
            }

            switch (ShowMenu("nuevoUsuario"))
            {
                case "1":
                    Console.Write("Ingrese la cédula del nuevo usuario: ");
                    string id = Console.ReadLine()!;
                    Console.Write("Ingrese los nombres del nuevo usuario: ");
                    string name = Console.ReadLine()!;
                    Console.Write("Ingrese los apellidos del nuevo usuario: ");
                    string lastname = Console.ReadLine()!;
                    Console.Write("Ingrese el teléfono del nuevo usuario: ");
                    string phone = Console.ReadLine()!;
                    Console.Write("Ingrese la dirección del nuevo usuario: ");
                    string address = Console.ReadLine()!;
                    Console.Clear();

                    SetUser(usersCount, id, name, lastname, phone, address);
                    usersCount++;

                    ShowMessage("Usuario agregado exitosamente.", false);
                    break;
                case "2":
                    loop = false;
                    break;
                default:
                    ShowMessage("Ingrese una opción del menú válida.", false);
                    break;
            }
        }
    }

    private static void EditUser()
    {
        if (usersCount == 0)
        {
            ShowMessage("No hay usuarios registrados.", false);
            return;
        }
        else
        {
            Console.WriteLine("Lista de usuarios:");
            for (int i = 0; i < usersCount; i++)
            {
                Console.WriteLine($"{GetUserId(i)} - {GetUserName(i)} {GetUserLastname(i)}");
            }
        }

        Console.Write($"\nIngrese el número de cédula del usuario que quiere editar: ");
        string input = Console.ReadLine()!;
        Console.Clear();
        bool found = false;
        for (int i = 0; i < usersCount; i++)
        {
            if (GetUserId(i) == input)
            {
                found = true;
                bool loop = true;
                while (loop)
                {
                    Console.Clear();
                    Console.WriteLine($"¿Qué desea editar del usuario: {GetUserId(i)}?");
                    Console.WriteLine($"1. Nombres: {GetUserName(i)}");
                    Console.WriteLine($"2. Apellidos: {GetUserLastname(i)}");
                    Console.WriteLine($"3. Teléfono: {GetUserPhone(i)}");
                    Console.WriteLine($"4. Dirección: {GetUserAddress(i)}");
                    Console.Write("Seleccione la opción que quiere editar (1-4): ");
                    string option = Console.ReadLine()!;
                    Console.Clear();
                    switch (option)
                    {
                        case "1":
                            Console.Write($"Editar nombres {GetUserName(i)} a: ");
                            string newName = Console.ReadLine()!;
                            SetUser(i, name: newName);
                            loop = false;
                            break;
                        case "2":
                            Console.Write($"Editar apellidos {GetUserLastname(i)} a: ");
                            string newLastname = Console.ReadLine()!;
                            SetUser(i, lastname: newLastname);
                            loop = false;
                            break;
                        case "3":
                            Console.Write($"Editar teléfono {GetUserPhone(i)} a: ");
                            string newPhone = Console.ReadLine()!;
                            SetUser(i, phone: newPhone);
                            loop = false;
                            break;
                        case "4":
                            Console.Write($"Editar dorección {GetUserAddress(i)} a: ");
                            string newAddress = Console.ReadLine()!;
                            SetUser(i, address: newAddress);
                            loop = false;
                            break;
                        default:
                            ShowMessage("Opción inválida.", false);
                            break;
                    }
                }
                break;
            }
        }
        if (!found)
        {
            ShowMessage("Usuario no encontrado.", false);
        }
    }

    private static void ClearUsers()
    {
        users = new string[5, 15];
        usersCount = 0;
    }

    private static string GetUserId(int index)
    {
        return users[0, index];
    }

    private static string GetUserName(int index)
    {
        return users[1, index];
    }

    private static string GetUserLastname(int index)
    {
        return users[2, index];
    }

    private static string GetUserPhone(int index)
    {
        return users[3, index];
    }

    private static string GetUserAddress(int index)
    {
        return users[4, index];
    }

    private static void SetUser(int index, string? id = null, string? name = null, string? lastname = null, string? phone = null, string? address = null)
    {
        if (id != null) users[0, index] = id;
        if (name != null) users[1, index] = name;
        if (lastname != null) users[2, index] = lastname;
        if (phone != null) users[3, index] = phone;
        if (address != null) users[4, index] = address;
    }


    private static void Items()
    {
        bool pass = true;
        while (pass)
        {
            Console.Clear();
            switch (ShowMenu("articulos"))
            {
                case "1":
                    ShowItem();
                    break;
                case "2":
                    AddItem();
                    break;
                case "3":
                    EditItem();
                    break;
                case "4":
                    pass = false;
                    break;
                default:
                    ShowMessage("Opción no válida.", false);
                    break;
            }
        }
    }

    private static void ShowItem()
    {
            if (itemsCount == 0)
            {
                ShowMessage("No hay artículos registrados.", false);
                return;
            }
            else
            {
                Console.WriteLine("Lista de artículos:");
                for (int j = 0; j < itemsCount; j++)
                {
                    Console.WriteLine($"{j + 1}. {GetItemName(j)} - Valor: ${GetItemPrice(j)} - Stock: {GetItemStock(j)}");
                }
            }
            ShowMessage("", false);
    }

    private static void AddItem()
    {
        bool loop = true;
        while (loop)
        {
            switch (ShowMenu("nuevoArticulo"))
            {
                case "1":
                    if (itemsCount == 15)
                    {
                        ShowMessage("No se pueden agregar más artículos. Límite alcanzado.", false);
                        return;
                    }
                    Console.Write("Ingrese el nombre del nuevo artículo: ");
                    string newItemName = Console.ReadLine()!;
                    int newPrice = 0;
                    int newStock = 0;
                    bool valid = false;
                    while (!valid)
                    {
                        Console.Write("Ingrese el valor unitario del nuevo artículo: ");
                        string price = Console.ReadLine()!;
                        if (int.TryParse(price, out int p) && p > 0)
                        {
                            newPrice = p;
                            valid = true;
                        }
                        else
                        {
                            Console.Clear();
                            ShowMessage("Entrada inválida. Por favor, ingrese un número entero positivo.", false);
                            Console.WriteLine($"Ingrese el nombre del nuevo artículo: {newItemName}");
                        }
                    }
                    valid = false;
                    while (!valid)
                    {
                        Console.Write("Ingrese la cantidad en stock del nuevo artículo: ");
                        string stock = Console.ReadLine()!;
                        if (int.TryParse(stock, out int s) && s > 0)
                        {
                            newStock = s;
                            valid = true;
                        }
                        else
                        {
                            Console.Clear();
                            ShowMessage("Entrada inválida. Por favor, ingrese un número entero positivo.", false);
                            Console.WriteLine($"Ingrese el nombre del nuevo artículo: {newItemName}");
                            Console.WriteLine($"Ingrese el valor unitario del nuevo artículo: {newPrice}");
                        }
                    }
                    SetItem(itemsCount, (itemsCount + 1).ToString(), newItemName, newPrice, newStock);
                    itemsCount++;
                    Console.Clear();
                    ShowMessage("Artículo agregado exitosamente.", false);
                    break;
                case "2":
                    loop = false;
                    break;
                default:
                    ShowMessage("Ingrese una opción del menú válida.", false);
                    break;
            }
        }
        
    }

    private static void EditItem()
    {
        if (itemsCount == 0)
        {
            ShowMessage("No hay artículos registrados.", false);
            return;
        }
        else
        {
            Console.WriteLine("Lista de artículos:");
            for (int i = 0; i < itemsCount; i++)
            {
                Console.WriteLine($"{GetItemId(i)} - {GetItemName(i)}");
            }
        }

        Console.Write($"\nIngrese el id del artículo que quiere editar: ");
        string input = Console.ReadLine()!;
        Console.Clear();
        bool found = false;
        for (int i = 0; i < itemsCount; i++)
        {
            if (GetItemId(i) == input)
            {
                found = true;
                bool loop = true;
                while (loop)
                {
                    Console.Clear();
                    Console.WriteLine($"¿Qué desea editar del artículo: {GetItemId(i)}?");
                    Console.WriteLine($"1. Nombre: {GetItemName(i)}");
                    Console.WriteLine($"2. Valor Unitario: {GetItemPrice(i)}");
                    Console.WriteLine($"3. Stock: {GetItemStock(i)}");
                    Console.Write("Seleccione la opción que quiere editar (1-3): ");
                    string option = Console.ReadLine()!;
                    Console.Clear();
                    switch (option)
                    {
                        case "1":
                            Console.Write($"Editar nombre {GetItemName(i)} a: ");
                            string newName = Console.ReadLine()!;
                            SetUser(i, name: newName);
                            loop = false;
                            break;
                        case "2":
                            bool valid = false;
                            while (!valid)
                            {
                                Console.Write($"Editar valor unitario {GetItemPrice(i)} a: ");
                                string price = Console.ReadLine()!;
                                if (int.TryParse(price, out int p) && p > 0)
                                {
                                    SetItem(i, price: p);
                                    valid = true;
                                    loop = false;
                                }
                                else
                                {
                                    Console.Clear();
                                    ShowMessage("Entrada inválida. Por favor, ingrese un número entero positivo.", false);
                                }
                            }
                            break;
                        case "3":
                            valid = false;
                            while (!valid)
                            {
                                Console.Write($"Editar stock {GetItemStock(i)} a: ");
                                string stock = Console.ReadLine()!;
                                if (int.TryParse(stock, out int s) && s > 0)
                                {
                                    SetItem(i, stock: s);
                                    valid = true;
                                    loop = false;
                                }
                                else
                                {
                                    Console.Clear();
                                    ShowMessage("Entrada inválida. Por favor, ingrese un número entero positivo.", false);
                                }
                            }
                            break;
                        default:
                            ShowMessage("Opción inválida.", false);
                            break;
                    }
                }
                break;
            }
        }
        if (!found)
        {
            ShowMessage("Artículo no encontrado.", false);
        }
    }

    private static void ClearItems()
    {
        items = new string[5, 15];
        itemsCount = 0;
    }

    private static string GetItemId(int index)
    {
        return items[0, index];
    }

    private static string GetItemName(int index)
    {
        return items[1, index];
    }

    private static int GetItemPrice(int index)
    {
        return int.Parse(items[2, index]);
    }

    private static int GetItemStock(int index)
    {
        return int.Parse(items[3, index]);
    }

    private static void SetItem(int index, string? id = null, string? name = null, int? price = null, int? stock = null)
    {
        if (id != null) items[0, index] = id;
        if (name != null) items[1, index] = name;
        if (price != null) items[2, index] = price.ToString()!;
        if (stock != null) items[3, index] = stock.ToString()!;
    }

    private static void Sales()
    {
        if (usersCount == 0 || itemsCount == 0)
        {
            ShowMessage("No hay usuarios o artículos para generar factura.", false);
        }
        else
        {
            bool pass = true;
            while (pass)
            {
                Console.Clear();
                switch (ShowMenu("ventas"))
                {
                    case "1":
                        string userBuying = "";
                        bool loop = true;
                        while (loop)
                        {
                            Console.WriteLine("Lista de usuarios:");
                            for (int i = 0; i < usersCount; i++)
                            {
                                Console.WriteLine($"{GetUserId(i)} - {GetUserName(i)} {GetUserLastname(i)}");
                            }
                            Console.Write($"\nIngrese el número de cédula del usuario comprador: ");
                            string input = Console.ReadLine()!;
                            Console.Clear();
                            bool found = false;
                            for (int i = 0; i < usersCount; i++)
                            {
                                if (GetUserId(i) == input)
                                {
                                    userBuying = $"{GetUserId(i)} - {GetUserName(i)} {GetUserLastname(i)}";
                                    found = true;
                                    loop = false;
                                    break;
                                }
                            }
                            if (!found)
                            {
                                ShowMessage("Usuario no encontrado.", false);
                            }
                        }

                        loop = true;
                        while (loop)
                        {
                            Console.Clear();
                            Console.WriteLine("Lista de artículos:");
                            for (int j = 0; j < itemsCount; j++)
                            {
                                Console.WriteLine($"{GetItemId(j)} - {GetItemName(j)}");
                            }

                            Console.Write($"Ingrese el id del artículo que quiere comprar: ");
                            string item = Console.ReadLine()!;
                            Console.Clear();

                            if (!int.TryParse(item, out int i) || i > itemsCount || i <= 0)
                            {
                                ShowMessage("Ingrese una opción del menú válida.", false);
                            }
                            else
                            {
                                i -= 1;
                                bool valid = false;
                                while (!valid)
                                {
                                    Console.WriteLine($"{GetItemName(i)} - ${GetItemPrice(i)} - Stock: {GetItemStock(i)}\n");
                                    Console.Write($"Ingrese la cantidad de unidades que desea comprar: ");
                                    string saleQuantity = Console.ReadLine()!;
                                    if (int.TryParse(saleQuantity, out int q) && q > 0 && q <= GetItemStock(i))
                                    {
                                        SetSoldItem(soldItemsCount, GetItemId(i), GetItemName(i), GetItemPrice(i), q, q * GetItemPrice(i));
                                        Console.Clear();
                                        Console.WriteLine($"Producto comprado. Total ${GetSoldItemTotal(soldItemsCount)}: {GetSoldItemName(soldItemsCount)} - {q} unidades a ${GetSoldItemPrice(soldItemsCount)} c/u.");
                                        soldItemsCount++;
                                        if (soldItemsCount == 10)
                                        {
                                            Console.Clear();
                                            Console.WriteLine($"Información del comprador: {userBuying}");
                                            Console.WriteLine("Productos Comprados:");
                                            int sold = 0;
                                            for (int j = 0; j < soldItemsCount; j++)
                                            {
                                                int soldItemPrice = GetSoldItemTotal(j);
                                                Console.WriteLine($"{GetSoldItemId(j)} - {GetSoldItemName(j)} - Valor Unitario: {GetSoldItemPrice(j)} - Cantidad: {GetSoldItemStock(j)} - Subtotal: ${soldItemPrice}");
                                                sold += soldItemPrice;
                                            }
                                            ShowMessage($"\nCompra exitosa. Valor total: ${sold}", true);
                                            ClearSoldItems();
                                            loop = false;
                                            pass = false;
                                        }
                                        else
                                        {
                                            bool loop1 = true;
                                            while (loop1)
                                            {
                                                switch (ShowMenu("nuevaVenta"))
                                                {
                                                    case "1":
                                                        loop1 = false;
                                                        break;
                                                    case "2":
                                                        Console.Clear();
                                                        Console.WriteLine($"Información del comprador: {userBuying}");
                                                        Console.WriteLine("Productos Comprados:");
                                                        int sold = 0;
                                                        for (int j = 0; j < soldItemsCount; j++)
                                                        {
                                                            int soldItemPrice = GetSoldItemTotal(j);
                                                            Console.WriteLine($"{GetSoldItemId(j)} - {GetSoldItemName(j)} - Valor Unitario: {GetSoldItemPrice(j)} - Cantidad: {GetSoldItemStock(j)} - Subtotal: ${soldItemPrice}");
                                                            sold += soldItemPrice;
                                                        }
                                                        ShowMessage($"\nCompra exitosa. Valor total: ${sold}", true);
                                                        ClearSoldItems();
                                                        loop = false;
                                                        loop1 = false;
                                                        pass = false;
                                                        break;
                                                    default:
                                                        ShowMessage("Opción no válida.", false);
                                                        break;
                                                }
                                            }
                                        }
                                        valid = true;
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        ShowMessage("Entrada inválida.", false);
                                    }
                                }
                            }
                        }
                        break;
                    case "2":
                        pass = false;
                        break;

                    default:
                        ShowMessage("Opción no válida.", true);
                        break;
                }
            }
        }
    }

    private static void ClearSoldItems()
    {
        soldItems = new string[5, 15];
        soldItemsCount = 0;
    }

    private static string GetSoldItemId(int index)
    {
        return soldItems[0, index];
    }

    private static string GetSoldItemName(int index)
    {
        return soldItems[1, index];
    }

    private static int GetSoldItemPrice(int index)
    {
        return int.Parse(soldItems[2, index]);
    }

    private static int GetSoldItemStock(int index)
    {
        return int.Parse(soldItems[3, index]);
    }

    private static int GetSoldItemTotal(int index)
    {
        return int.Parse(soldItems[4, index]);
    }

    private static void SetSoldItem(int index, string? id = null, string? name = null, int? price = null, int? stock = null, int? total = null)
    {
        if (id != null) soldItems[0, index] = id;
        if (name != null) soldItems[1, index] = name;
        if (price != null) soldItems[2, index] = price.ToString()!;
        if (stock != null) soldItems[3, index] = stock.ToString()!;
        if (total != null) soldItems[4, index] = total.ToString()!;
    }

    private static void ShowMessage(string message, bool isContinue)
    {
        Console.Write($"{message} \n\nPresione cualquier tecla para " + (isContinue ? "continuar" : "volver") + ". . . ");
        Console.ReadKey();
        Console.Clear();
    }

    private static string ShowMenu(string menu)
    {
        switch (menu)
        {
            case "main":
                Console.WriteLine("===== MENÚ PRINCIPAL =====");
                Console.WriteLine("1. Gestión de usuarios");
                Console.WriteLine("2. Gestión de artículos");
                Console.WriteLine("3. Gestión de ventas");
                Console.WriteLine("4. Salir del programa");
                Console.Write("Seleccione una opción (1-4): ");
                break;
            case "usuarios":
                Console.WriteLine("===== GESTIÓN DE USUARIOS =====");
                Console.WriteLine("1. Ver lista de usuarios");
                Console.WriteLine("2. Nuevo usuario");
                Console.WriteLine("3. Editar información de usuario");
                Console.WriteLine("4. Salir de Gestión de usuarios");
                Console.Write("Seleccione una opción (1-4): ");
                break;
            case "nuevoUsuario":
                Console.WriteLine("===== NUEVO USUARIO =====");
                Console.WriteLine("1. Crear usuario");
                Console.WriteLine("2. Salir");
                Console.Write("Seleccione una opción (1-2): ");
                break;
            case "articulos":
                Console.WriteLine("===== GESTIÓN DE ARTÍCULOS =====");
                Console.WriteLine("1. Ver lista de artículos");
                Console.WriteLine("2. Nuevo artículo");
                Console.WriteLine("3. Editar información del artículo");
                Console.WriteLine("4. Salir de Gestión de artículos");
                Console.Write("Seleccione una opción (1-4): ");
                break;
            case "nuevoArticulo":
                Console.WriteLine("===== NUEVO ARTÍCULO =====");
                Console.WriteLine("1. Crear artículo");
                Console.WriteLine("2. Salir");
                Console.Write("Seleccione una opción (1-2): ");
                break;
            case "ventas":
                Console.WriteLine("===== GESTIÓN DE VENTAS =====");
                Console.WriteLine("1. Generar factura");
                Console.WriteLine("2. Salir de Gestión de ventas");
                Console.Write("Seleccione una opción (1-2): ");
                break;
            case "nuevaVenta":
                Console.WriteLine("¿Desea ingresar un nuevo producto a su compra?");
                Console.WriteLine("1. Si");
                Console.WriteLine("2. No");
                Console.Write("Seleccione una opción (1-2): ");
                break;
        }
        string option = Console.ReadLine()!;
        Console.Clear();
        return option;

    }
}