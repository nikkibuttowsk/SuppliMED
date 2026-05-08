<h1 align="center">
        <img width="1100" height="250" alt="supplimedlogo" src="https://github.com/user-attachments/assets/bfd5a536-7a62-4d36-abf1-67d9304b9e49" /
</h1>

<h2 align="center">
  A Medicine Inventory System powered by C# and SQL, with a modern HTML and CSS interface.
</h2>

<div align="center">

<img src="https://img.shields.io/badge/Language-C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white">
<img src="https://img.shields.io/badge/Language-HTML-E34F26?style=for-the-badge&logo=html5&logoColor=white">
<img src="https://img.shields.io/badge/Language-CSS-1572B6?style=for-the-badge&logo=css3&logoColor=white">
<img src="https://img.shields.io/badge/Language-SQL-4479A1?style=for-the-badge&logo=mysql&logoColor=white">
</div>


## 📖 About the Project

<div align="justify">

This project presents a Medicine Inventory System designed to efficiently manage and organize medical supplies. It is developed using C# for the backend logic, with SQL serving as the database for secure and structured data storage. The system also features a user-friendly interface built with HTML and CSS to ensure ease of use and accessibility.

The main goal of this system is to help small pharmacies, non-government organizations (NGOs), barangay health centers, and volunteer groups improve the way they track and manage medicines. By providing an organized and reliable inventory process, the system aims to reduce errors, prevent stock shortages, and support better decision-making in distributing medical supplies to communities in need.

<br/>

## ✨ Key Features

<details>
  <summary> Role-Based Access Control (RBAC) </summary>

### `Role-Based Access Control (RBAC)`
The system implements a role-based authentication mechanism that differentiates administrative users from regular staff accounts. Authentication is handled through the `AuthService.cs` service located in the backend architecture. This implementation allows the system to identify whether the authenticated account belongs to an administrator or staff member, enabling role-based access and permissions within the inventory system.

* **`Admin`** – Has elevated privileges for managing inventory and system operations.
* **`Staff`** – Has standard access for inventory-related tasks and monitoring.

</details>

<details>
  <summary> Inventory Tracking </summary>

### `Inventory Tracking`
The system implements a structured inventory tracking mechanism that allows real-time monitoring of medical supplies and equipment within the database. It ensures that all stock movements such as additions, updates, and deductions are properly recorded through the system’s backend services. This implementation helps maintain accurate inventory records, reduces the risk of shortages, and improves overall supply management efficiency.

* **`Stock Monitoring`** – Tracks available quantities of medicines and supplies in real time.
* **`Expiration Tracking`** – Identifies items nearing expiration to prevent unsafe usage.
* **`Low Stock Alerts`** – Notifies users when inventory falls below safe thresholds.

</details>

<details>
  <summary> Real-time Search & Filtering </summary>

### `Real-time Search & Filtering`
The system implements a real-time search and filtering mechanism that allows users to instantly locate specific inventory records as they type in the search bar. This functionality is handled through the frontend script, which continuously queries and filters displayed data without requiring page reloads. It improves usability by making large datasets easier to navigate and manage efficiently.

* **`Live Search`** – Dynamically filters results as the user types input.
* **`Instant Filtering`** – Narrows down inventory records based on keywords.
* **`Improved Navigation`** – Enhances speed and accuracy in finding specific data.

</details>

<details>
  <summary>Database Integration</summary>

### `Database Integration`
The system integrates a SQL-based database to ensure reliable storage and retrieval of all inventory data. It supports secure processing of transactions such as adding, updating, and deleting records while maintaining data consistency across the system. This implementation improves data management efficiency and ensures that all information is properly stored and easily accessible when needed.

* **`SQL Database`** – Provides structured and secure storage of system data.
* **`Data Consistency`** – Maintains accurate and synchronized records across all operations.
* **`Reliable Retrieval`** – Enables fast and efficient access to stored inventory information.

</details>

<details>
  <summary> Audit Log </summary>

### `Audit Log`
The system implements an automated audit logging mechanism that continuously records user activities and system transactions. It tracks detailed information regarding who performed an action, what specific changes were made, and the exact time the event occurred. This implementation ensures accountability among staff and administrators, aids in troubleshooting, and enhances overall system security by maintaining a transparent history of all data modifications.

* **`Activity Tracking`** – Monitors and records all critical system operations, including stock additions, updates, and deletions.
* **`Timestamped Records`** – Accurately logs the exact date and time of every recorded transaction for precise tracking.
* **`User Accountability`** – Links specific actions directly to individual administrator or staff accounts to ensure system security.

</details>

</div>
<br/>


## 📂 Project Structure 

<div align="justify">

The **SuppliMed Medicine Inventory System** is structured into two distinct environments: a dedicated frontend folder for all client-side assets (HTML, CSS, JavaScript, and media files) and a separate backend folder for the ASP.NET (C#) architecture. Below is the project structure overview of the system:

</p>
<details>
  <summary>Project Diagram</summary>
<pre>
SuppliMed/
│
├── 📁 frontend/
│   │
│   ├── 📁 css/
│   │   ├── audit.css
│   │   ├── dashboard.css
│   │   ├── inventory.css
│   │   ├── login.css
│   │   ├── mainLayout.css
│   │   └── modals.css
│   │
│   ├── 📁 icons/
│   │   ├── 0.jpg
│   │   ├── 0.png
│   │   ├── addSupply.png
│   │   ├── audit.png
│   │   ├── deleteSupply.png
│   │   ├── expired.png
│   │   ├── home.png
│   │   ├── inventory.png
│   │   ├── logout.png
│   │   ├── lowStock.png
│   │   ├── totalSupplies.png
│   │   └── updateSupply.png
│   │
│   ├── 📁 js/
│   │   ├── action.js
│   │   ├── app.js
│   │   ├── audit.js
│   │   ├── auth.js
│   │   ├── dashboard.js
│   │   ├── inventory.js
│   │   └── modals.js
│   │
│   ├── 🖼️ background.png
│   ├── 📄 dashboard.html
│   └── 📄 login.html
│
├── 📁 backend/
│   │
│   ├── 📁 AppCore/
│   │   │
│   │   ├── 📁 Data/
│   │   │   ├── AppDbContext.cs
│   │   │   └── AppDbContextFactory.cs
│   │   │
│   │   ├── 📁 Interfaces/
│   │   │   └── IInventoryService.cs
│   │   │
│   │   ├── 📁 Migrations/
│   │   ├── 📁 Models/
│   │   ├── 📁 Services/
│   │   └── 📄 AppCore.csproj
│   │
│   ├── 📁 SuppliMed.Api/
│   │   │
│   │   ├── 📁 Controllers/
│   │   │   ├── AuthController.cs
│   │   │   ├── InventoryController.cs
│   │   │   └── LogoutController.cs
│   │   │
│   │   ├── 📁 DTOs/
│   │   │   ├── AuditLogDTO.cs
│   │   │   ├── InventoryDTO.cs
│   │   │   └── UserDTO.cs
│   │   │
│   │   ├── 📁 Properties/
│   │   │   └── launchSettings.json
│   │   │
│   │   ├── 📄 Program.cs
│   │   ├── 📄 appsettings.json
│   │   ├── 📄 appsettings.Development.json
│   │   ├── 📄 SuppliMed.Api.csproj
│   │   └── 📄 SuppliMed.Api.http
│   │
│   ├── 📁 TestApp/
│   │   ├── TestApp.csproj
│   │   └── testcheck.cs
│   │
│   ├── 📄 .gitignore
│   └── 📄 SuppliMed.slnx
│
└── 📄 README.md
</pre>
</details>

<details>
  <summary> Frontend </summary>
        
### `Frontend`
The frontend contains all client-side resources—including styles, scripts, media assets, and HTML pages—that power the visual presentation and interactive user experience of SuppliMed.

#### CSS Stylesheets
Contains all styling resources responsible for the visual presentation of the system. These stylesheets define the layout, colors, typography, spacing, and responsiveness of the user interface to ensure a consistent and user-friendly design.
* **`dashboard.css`** – Dashboard layout and components.
* **`inventory.css`** – Inventory table and controls.
* **`login.css`** – Login page styling.
* **`audit.css`** – Audit logs interface.
* **`mainLayout.css`** – Global layout (navbar, structure).
* **`modals.css`** – Modal UI components.

#### JavaScript Modules
Contains all client-side scripts responsible for handling system functionality, interactivity, and communication with the backend. These modules manage user actions, dynamic content updates, and API interactions.
* **`app.js`** – Core application logic.
* **`auth.js`** – User authentication handling.
* **`inventory.js`** – Inventory CRUD operations.
* **`dashboard.js`** – Dashboard data updates.
* **`audit.js`** – Audit trail tracking.
* **`action.js`** – General UI actions/events.
* **`modals.js`** – Modal interactions and forms.

#### HTML Pages
Contains the core user interface pages of the system, serving as the structural foundation of the application. These pages define the layout and integrate styles (CSS) and functionality (JavaScript) to deliver an interactive user experience.
* **`login.html`** – Entry point of the system, typically used for login or initial access.
* **`dashboard.html`** – Main interface displaying system overview, navigation, and key data.

#### Icons Directory
This directory contains all the graphical assets and standard imagery used to build the visual components of the SuppliMed user interface. 
* **`0.jpg`** – Alternate or legacy default avatar for user profiles.
* **`0.png`** – Default avatar icon used for user profile pictures.
* **`addSupply.png`** – Icon representing the action to register a new medical supply.
* **`audit.png`** – Icon used for the Audit Logs navigation link or header.
* **`deleteSupply.png`** – Icon representing the action to permanently remove a medical supply.
* **`expired.png`** – Status icon used to highlight expired or expiring stock on the dashboard.
* **`home.png`** – Navigation icon used for the main Dashboard view.
* **`inventory.png`** – Navigation icon used for the Inventory Management module.
* **`logout.png`** – Icon used for the system logout button.
* **`lowStock.png`** – Status icon used to flag items that have dropped below their minimum stock threshold.
* **`totalSupplies.png`** – Dashboard icon representing the aggregate count of all supplies in the system.
* **`updateSupply.png`** – Icon representing the action to adjust or restock an existing supply.
</details>

<details>
  <summary> Backend </summary>
        
### `Backend`
The backend follows a clean architecture pattern, separating the core business domain (`AppCore`) from the implementation of the web interface (`SuppliMed.Api`).

#### AppCore (Domain & Data Layer)
This layer manages the application's state, database schema, and core logic.
* **`Data/`** – Contains the `AppDbContext.cs` for Entity Framework Core operations.
* **`Interfaces/`** – Defines `IInventoryService.cs` to decouple the API from the implementation.
* **`Models/`** – The heart of the system, representing our database entities and business objects:
  * `Admin.cs` & `Staff.cs` – Specialized user roles.
  * `Medicine.cs` & `MedicalSupply.cs` – Core inventory items.
  * `Batch.cs` & `Equipment.cs` – Advanced tracking for supplies.
  * `AuditLog.cs` & `LoginAttempt.cs` – Security and tracking entities.
* **`Services/`** – The "brains" of the operation:
  * `AuthService.cs` – Handles logic for secure sign-ins.
  * `InventoryServices.cs` – Manages complex stock logic.
  * `InventoryDataSeeder.cs` – Populates the database with initial medicine records.

#### SuppliMed.Api (Web API Layer)
The bridge between our database logic and the HTML/JS frontend.
* **`Controllers/`** – Handles incoming web requests:
  * `AuthController.cs` – Validates login credentials.
  * `InventoryController.cs` – Exposes stock data to the UI.
  * `LogoutController.cs` – Securely clears user sessions.
* **`DTOs/`** – Data Transfer Objects like `AuditLogDTO.cs` ensure that we only send necessary data across the network, optimizing speed and security.
* **`Program.cs`** – Configures the web server and hooks up dependency injection.
* **`appsettings.json`** – Stores the database connection string and API settings.

#### Development & Tooling
* **`Migrations/`** – Records of database schema updates (Initial, AddTable, etc.).
* **`launchSettings.json`** – Configures how the API starts up during development.
* **`.gitignore`** – Keeps sensitive files and local builds out of the repository.

</div>
<br/>
</details>

## 🚀 Getting Started

<div align="justify">
Quick setup guide for running SuppliMed locally.
<p> </p>
<details>
  <summary> How to install and setup the program </summary>

### 🛠️ Installation & Setup

#### 1. Clone the Repository
```bash
git clone https://github.com/your-username/SuppliMed.git](https://github.com/nikkibuttowsk/SuppliMED.git
cd SuppliMED/SuppliMed/backend
```



#### 2. Setup the Database
Run the correct Entity Framework command:

```bash
dotnet ef database update --project AppCore --startup-project SuppliMed.Api
```

> This will create and update the database using your backend configuration.



#### 3. Start Required Services
Make sure your local server is running:

- Open **XAMPP**
- Start:
  - Apache
  - MySQL



#### 4. Run the Backend (API)
Inside the backend folder, run:

```bash
dotnet run --project SuppliMed.Api
```



#### 5. Run the Frontend (UI)

- Go to the frontend folder
- Right-click `login.html`
- Click **"Open with Live Server"**


#### 6. Access the System

- The system will open in your browser
- Login using your account


#### ⚠️ Note
- Make sure backend is running before opening frontend  
- Ensure API URL in your JS matches backend port  
- XAMPP must be running for database connection  

</div>
<br/>

</details>

## 🍵 Implementation of Object-Oriented Programming (OOP) Principles
<div align="justify">

The SuppliMed project demonstrates the implementation of Object-Oriented Programming (OOP) principles in C# to ensure a structured, maintainable, and scalable system design.

<details>
  <summary> Encapsulation </summary>

### `Encapsulation`
Encapsulation is implemented by protecting core data and business logic from direct, unsafe modifications. Instead of allowing API controllers to directly modify database values, sensitive operations are handled inside the `InventoryServices` class. This ensures that stock updates are always validated, logged, and processed safely.

Example:
```csharp

public class InventoryServices
{
    // The raw database context is protected from outside interference
    private readonly AppDbContext _context;

    // Controlled access: Controllers MUST use this method to change stock,
    // ensuring audit logs and validation are always handled correctly.
    public void ProcessStockUpdate(string id, int quantity, User currentUser, string batchNumber)
    {
        var supply = GetSupplyById(id);
        if (supply == null) throw new Exception("Supply not found.");

        supply.Quantity += quantity;

        // Encapsulated audit logging
        LogAuditAction(currentUser.Username, "UPDATE", supply.Name, $"Stock adjusted by {quantity}");
        _context.SaveChanges();
    }
}
```

</details>

<details>
  <summary> Inheritance </summary>

### `Inheritance`
Inheritance is implemented through a class hierarchy that eliminates redundancy. Shared properties such as `Id`, `Name`, `Brand`, and `Quantity` are stored in a base class, while specific entities extend this base structure.

Example:

```csharp
public abstract class MedicalSupply
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Brand { get; set; }
    public int Quantity { get; set; }
    public int MinimumStock { get; set; }
}

// Equipment inherits base properties
public class Equipment : MedicalSupply
{
    public string SerialNumber { get; set; }
}

// Medicine inherits base properties and adds batch tracking
public class Medicine : MedicalSupply
{
    public List<Batch> Batches { get; set; }
}
```
</details>

<details>
  <summary> Polymorphism </summary>

### `Polymorphism`
Polymorphism is implemented using method overriding, allowing different inventory types to behave differently while sharing a common structure. This is used when displaying expiry dates depending on the type of supply.

Example:

```csharp
public abstract class MedicalSupply
{
    // Default behavior
    public virtual string GetDisplayExpiryDate()
    {
        return "N/A";
    }
}

public class Medicine : MedicalSupply
{
    public List<Batch> Batches { get; set; }

    // Overrides base behavior
    public override string GetDisplayExpiryDate()
    {
        var nextBatch = Batches.OrderBy(b => b.ExpirationDate).FirstOrDefault();
        return nextBatch != null
            ? nextBatch.ExpirationDate.ToString("MMM dd, yyyy")
            : "N/A";
    }
}
```
</details>

<details>
  <summary> Abstraction </summary>

### `Abstraction`
Abstraction is implemented by hiding complex logic inside the service layer. The controller only interacts with simple methods, while internal processes such as ID generation and validation are handled behind the scenes.

Example:

```csharp
public class InventoryController : ControllerBase
{
    private readonly InventoryServices _inventoryService;

    [HttpPost("add")]
    public IActionResult AddNewSupply([FromBody] SupplyRequest request)
    {
        // The controller does not know how IDs are generated internally
        string newId = _inventoryService.GenerateNextId(request.Category);

        return Ok(new { message = "Supply added", id = newId });
    }
}
```
</details>

</div>
<br/>


## 🖥️ Program Snapshots
<div align="justify">

This section provides a visual overview of the SuppliMed user interface. 
<br/>

<details>
  <summary> Login Form </summary>
<p> </p>
<img width="1331" height="883" alt="login-admin" src="https://github.com/user-attachments/assets/4c1bab8d-98e9-4213-ab33-96507723fe19" />
</details>

<details>
  <summary> Dashboard Form </summary>
<p> </p>
        
### `Admin View`
<img width="1516" height="883" alt="dashboard-adminview" src="https://github.com/user-attachments/assets/87f3105b-9a7b-4b17-af99-620d44c6bcff" />
<p> </p>

### `Staff View`
<img width="1516" height="883" alt="dashboard-staffview" src="https://github.com/user-attachments/assets/d830d749-dac5-48cb-916c-bd769f871819" />
</details>

<details>
  <summary> Inventory Form </summary>
<p> </p>
        
### `Admin View`
<img width="1516" height="883" alt="inventory-adminview" src="https://github.com/user-attachments/assets/aa44fcdf-0a69-4397-be1a-99482865bb92" />
<p> </p>

### `Staff View`
<img width="1516" height="883" alt="inventory-staffview" src="https://github.com/user-attachments/assets/384ec6df-02ae-4ba0-80a0-3a23862a3be6" />
</details>

<details>
  <summary> Audit Log Form </summary>
<p> </p>
<img width="1516" height="883" alt="auditlog" src="https://github.com/user-attachments/assets/4cdd4c17-940e-400d-9ecf-b10cead59ca0" />
</details>
        
<details>
  <summary> Snapshots of Features </summary>
<p> </p>

### `Add Supply`
<img width="1516" height="883" alt="addsupplyForm" src="https://github.com/user-attachments/assets/0da65499-1be7-44a0-8948-50f7d5539ed6" />
<p> </p>

### `Delete Supply`
<img width="1516" height="883" alt="deletesupplyForm" src="https://github.com/user-attachments/assets/9948d0ec-5ce2-4f72-884e-459e8c369132" />
<p> </p>

### `Update Supply`
<img width="1516" height="883" alt="updatesupplyForm" src="https://github.com/user-attachments/assets/05ebade9-98c8-49f8-9621-5538fc04bf8d" />
<p> </p>

### `Inventory Search`
<img width="1516" height="883" alt="inventory-searchName" src="https://github.com/user-attachments/assets/f65b911f-2e98-4a6b-964f-2b8102013bc7" />
<p> </p>

### `Audit Log Search`
<img width="1516" height="883" alt="auditlog-searchUser" src="https://github.com/user-attachments/assets/c4c8977c-084d-4f17-a0ce-cd3571682164" />
<p> </p>

### `Logout`
<img width="1516" height="883" alt="logout" src="https://github.com/user-attachments/assets/3a3137e4-33a2-44a7-8572-8ec5f439aae9" />
<p> </p>

</details>



<br/>
</div>
<h3 align="center">CS 2204 - Group 1</h3>

<div align="center">

| Role | Members |
| :---: | :---: |
| **Project Manager / Lead Developer** | Nikki C. Limboc |
| **GUI Developer** | Rex Anthony C. Castillano |

</div>
<br/>
