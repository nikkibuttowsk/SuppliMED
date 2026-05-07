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


## 📖 About the Project

<div align="justify">

This project presents a Medicine Inventory System designed to efficiently manage and organize medical supplies. It is developed using C# for the backend logic, with SQL serving as the database for secure and structured data storage. The system also features a user-friendly interface built with HTML and CSS to ensure ease of use and accessibility.

The main goal of this system is to help small pharmacies, non-government organizations (NGOs), barangay health centers, and volunteer groups improve the way they track and manage medicines. By providing an organized and reliable inventory process, the system aims to reduce errors, prevent stock shortages, and support better decision-making in distributing medical supplies to communities in need.

<br/>

## ✨ Key Features

### `Role-Based Access Control (RBAC)`
The system implements a role-based authentication mechanism that differentiates administrative users from regular staff accounts. Authentication is handled through the `AuthService.cs` service located in the backend architecture. This implementation allows the system to identify whether the authenticated account belongs to an administrator or staff member, enabling role-based access and permissions within the inventory system.

* **`Admin`** – Has elevated privileges for managing inventory and system operations.
* **`Staff`** – Has standard access for inventory-related tasks and monitoring.

### `Inventory Tracking`
The system implements a structured inventory tracking mechanism that allows real-time monitoring of medical supplies and equipment within the database. It ensures that all stock movements such as additions, updates, and deductions are properly recorded through the system’s backend services. This implementation helps maintain accurate inventory records, reduces the risk of shortages, and improves overall supply management efficiency.

* **`Stock Monitoring`** – Tracks available quantities of medicines and supplies in real time.
* **`Expiration Tracking`** – Identifies items nearing expiration to prevent unsafe usage.
* **`Low Stock Alerts`** – Notifies users when inventory falls below safe thresholds.

### `Real-time Search & Filtering`
The system implements a real-time search and filtering mechanism that allows users to instantly locate specific inventory records as they type in the search bar. This functionality is handled through the frontend script, which continuously queries and filters displayed data without requiring page reloads. It improves usability by making large datasets easier to navigate and manage efficiently.

* **`Live Search`** – Dynamically filters results as the user types input.
* **`Instant Filtering`** – Narrows down inventory records based on keywords.
* **`Improved Navigation`** – Enhances speed and accuracy in finding specific data.


### `Database Integration`
The system integrates a SQL-based database to ensure reliable storage and retrieval of all inventory data. It supports secure processing of transactions such as adding, updating, and deleting records while maintaining data consistency across the system. This implementation improves data management efficiency and ensures that all information is properly stored and easily accessible when needed.

* **`SQL Database`** – Provides structured and secure storage of system data.
* **`Data Consistency`** – Maintains accurate and synchronized records across all operations.
* **`Reliable Retrieval`** – Enables fast and efficient access to stored inventory information.

### `Audit Log`
The system implements an automated audit logging mechanism that continuously records user activities and system transactions. It tracks detailed information regarding who performed an action, what specific changes were made, and the exact time the event occurred. This implementation ensures accountability among staff and administrators, aids in troubleshooting, and enhances overall system security by maintaining a transparent history of all data modifications.

* **`Activity Tracking`** – Monitors and records all critical system operations, including stock additions, updates, and deletions.
* **`Timestamped Records`** – Accurately logs the exact date and time of every recorded transaction for precise tracking.
* **`User Accountability`** – Links specific actions directly to individual administrator or staff accounts to ensure system security.

<br/>

## 📂 Project Structure 

<div align="justify">

The **SuppliMed Medicine Inventory System** is structured into two distinct environments: a dedicated frontend folder for all client-side assets (HTML, CSS, JavaScript, and media files) and a separate backend folder for the ASP.NET (C#) architecture. Below is the project structure overview of the system:

</p>

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
│   │   ├── 0.png
│   │   ├── 1-default.svg
│   │   ├── 2-default.svg
│   │   ├── 3-default.svg
│   │   ├── 3.png
│   │   ├── 5-default.svg
│   │   └── medickit.png
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
│   └── 📄 index.html
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

### 🖥️ Frontend
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
* **`index.html`** – Entry point of the system, typically used for login or initial access.
* **`dashboard.html`** – Main interface displaying system overview, navigation, and key data.

#### Icons Directory
This directory contains all the graphical assets, vector graphics, and standard imagery used to build the visual components of the SuppliMed user interface. 
* **`0.png`** – Default avatar icon used for user profile pictures.
* **`1-default.svg`** – Vector icon used for the Dashboard navigation link.
* **`2-default.svg`** – Vector icon used for the Inventory Management module.
* **`3-default.svg`** – Vector icon used for the Audit Logs or User Settings.
* **`5-default.svg`** – Vector icon used for system configuration or logout actions.


### ⚙️ Backend
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

<div align="center">


```html
<div>
  {{#request src="/api/my-component.html"}}
    {{#indicator trigger="pending"}}
      <p>Loading...</p>
    {{/indicator}}
  {{/request}}
</div>
```

Try HMPL online at: [hmpl-playground](https://codesandbox.io/p/sandbox/hmpl-playground-hz6k9t?file=%2Findex.html%3A13%2C44)

## Basic usage

```javascript
import hmpl from "hmpl-js";

const templateFn = hmpl.compile(
  `<div>
      <button data-action="increment" id="btn">Click!</button>
      <div>Clicks: {{#request src="/api/clicks" after="click:#btn"}}{{/request}}</div>
  </div>`
);

const clicker = templateFn(({ request: { event } }) => ({
  body: JSON.stringify({ action: event.target.getAttribute("data-action") })
})).response;

document.querySelector("#app").append(clicker);
```

<details>
<summary>Explain this!</summary>

```js
import hmpl from "hmpl-js"; // Import the HMPL library

// Compile an HMPL template with dynamic behavior
const templateFn = hmpl.compile(
  `<div>
      <button data-action="increment" id="btn">Click!</button>
      <!-- This div will update with the click count from /api/clicks -->
      <div>Clicks: {{#request src="/api/clicks" after="click:#btn"}}{{/request}}</div>
      <!-- Also, you can write in short: {{#r src="..."}}{{/r}} -->
  </div>`
);

// Generate a response handler for the template
// In the original object, we will have the following: { response: div, status: 200 }
const clicker = templateFn(({ request: { event } }) => ({
  // Send a JSON payload with the action from the button's data attribute
  body: JSON.stringify({ action: event.target.getAttribute("data-action") })
})).response;

// Append the dynamically generated element to the #app container
document.querySelector("#app").append(clicker);
```

In this example, we create a dynamic clicker component in which, when a `button` is pressed, we will receive the value of the current clicks that will come from the server. The advantage of this approach is that we can take out not only data in the form of `Text`, but also entire components and even pages!

</details>

## Usage with DOM

If you need an option without using js, then by connecting the additional [hmpl-dom](https://www.npmjs.com/package/hmpl-dom) module you can do this.

```html
<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Example</title>
  </head>
  <body>
    <main>
      <template hmpl>
        <div>
          {{#request src="/api/my-component.html"}}
            {{#indicator trigger="pending"}}
              <p>Loading...</p>
            {{/indicator}}
          {{/request}}
        </div>
      </template>
    </main>
    <script src="https://unpkg.com/json5/dist/index.min.js"></script>
    <script src="https://unpkg.com/dompurify/dist/purify.min.js"></script>
    <script src="https://unpkg.com/hmpl-js/dist/hmpl.min.js"></script>
    <script src="https://unpkg.com/hmpl-dom/dist/hmpl-dom.min.js"></script>
  </body>
</html>
```

This way, components from the server are mounted into the DOM without having to add them manually.

## Why HMPL?

Using template language capabilities, you can multiply reduce the size of the application bundle. Full customization of the request based on the modern `fetch` standard, as well as support for all the functionality necessary for modern work in applications (request indicator, sending by event, automatic generation of `body` for the `form`, caching) and the syntax of the object in the markup, which requires a minimum number of characters, will help to build interaction with the server and client as efficiently as possible. App size [comparison](https://github.com/hmpl-language/app-size-comparison) (bytes):

<div>
    <a href="https://github.com/hmpl-language/app-size-comparison">
        <img src="https://raw.githubusercontent.com/hmpl-language/media/refs/heads/main/comparison.png" alt="App size comparison">
    </a>
</div>

Also, HMPL can be a great alternative to popular tools such as HTMX and Alpine.js.

## Features

- **Customizable**: Send a custom request to the server when receiving the UI
- **Memory Preserving**: Reduce file sizes on the client by several times
- **Based on Fetch API**: Use a modern standard instead of `XMLHTTPRequest`
- **Server-oriented**: Work with the server directly through markup and with a little js
- **Generate thousands of DOM nodes from a single template**: Work with large components not only on the server but also on the client
- **Simple**: Get ready-made UI from the server by writing a couple of lines of familiar object syntax
- **Protected from XSS attacks**: Enable incoming server HTML sanitization with [DOMPurify](https://www.npmjs.com/package/dompurify) and work with it safely
- **Flexible**: Can be used in almost any project due to not only working through a script, but also working in files with the `.hmpl` extension
- **Integrated with JSON5**: Flexible writing of objects by [docs](https://hmpl-lang.dev/introduction) as in vanilla js, as well as the reliability of the parser used by millions of people
- **Small bundle size**: Lots of functionality in a couple of kilobytes

## Installation

hmpl can be installed in several ways, which are described in this section. This tool is a simple javascript file that is connected in the usual way through a `script`, or using the `import` construct in an environment that supports this (webpack build, parcel build etc.). 
> [!NOTE]
> Starting with version `2.2.0`, the JSON5 module needs to be connected, and starting with version `2.2.5`, the DOMPurify module also needs to be connected. The first and easiest way is to install using a CDN.

### Package Manager

This method involves downloading through npm or other package managers.

```bash
npm i hmpl-js
```

Along the path `node_modules/hmpl/dist` you can find two files that contain a regular js file and a minified one.

### CDN

This method involves connecting the file through a third-party resource, which provides the ability to obtain a javascript file from npm via a link.

```html
<script src="https://unpkg.com/json5/dist/index.min.js"></script>
<script src="https://unpkg.com/dompurify/dist/purify.min.js"></script>
<script src="https://unpkg.com/hmpl-js/dist/hmpl.min.js"></script>
<!--   
  integrity="..."
  crossorigin="anonymous"
-->
```

This resource could be unpkg, skypack or other resources. The examples include unpkg simply because it is one of the most popular and its url by characters is not so long.

### Through the starter template project

There is a [starter project](https://github.com/hmpl-language/hello-hmpl-starter) on Vite.

```sh
npx degit hmpl-language/hello-hmpl-starter hello-hmpl
```

Based on it, you can make web applications.

## Official Tools

<table cellpadding="10" cellspacing="0" border="0">
    <tr>
        <td>
            <img src="https://raw.githubusercontent.com/hmpl-language/media/refs/heads/main/VS%20Code.svg" width="100" alt="vs-code-extension">
        </td>
        <td>
            <a href="https://marketplace.visualstudio.com/items?itemName=hmpljs.hmpl"><b>VS Code Extension</b></a><br>
            <i>Syntax highlighting and tools for HMPL</i>
        </td>
        <td>
            <img src="https://raw.githubusercontent.com/hmpl-language/media/refs/heads/main/vite.png" width="100" alt="vite-plugin-hmpl">
        </td>
        <td>
            <a href="https://www.npmjs.com/package/vite-plugin-hmpl"><b>Vite Plugin</b></a><br>
            <i>Seamless .hmpl integration with Vite</i>
        </td>
    </tr>
    <tr>
        <td>
            <img src="https://raw.githubusercontent.com/hmpl-language/media/refs/heads/main/Webpack.svg" width="100" alt="hmpl-loader">
        </td>
        <td>
            <a href="https://www.npmjs.com/package/hmpl-loader"><b>Webpack Loader</b></a><br>
            <i>Compile .hmpl files inside Webpack</i>
        </td>
    </tr>
</table>

But we will be glad if you make your own :)

## Community support

The [documentation](https://hmpl-lang.dev/introduction) contains main information on how the HMPL template language works. If you have any questions about how HMPL works, you can use the following resources:

- [GitHub](https://github.com/hmpl-language/hmpl) - In the discussion and issues sections you can ask any question you are interested in
- [Discord](https://discord.gg/KFunMep36n) - You can ask your question in the thematic channel "support"
- [𝕏 (Twitter)](https://x.com/hmpljs) - There is a lot of interesting stuff there, concerning the template language and not only :)

You can also ask your question on [Stack Overflow](https://stackoverflow.com/) and address it in the resources described above.

## Contribution

We have a [Contributing Guide](https://github.com/hmpl-language/hmpl/blob/main/CONTRIBUTING.md) that describes the main steps for contributing to the project.

Thank you to all the people who have already contributed to HMPL, or related projects!

## Star History

[![Star History Chart](https://api.star-history.com/svg?repos=hmpl-language/hmpl&type=date&legend=top-left)](https://www.star-history.com/#hmpl-language/hmpl&type=date&legend=top-left)

## Roadmap

The project has a [roadmap](https://github.com/orgs/hmpl-language/projects/5) where you can see the plans for future development.

## License

Released under the [MIT](https://github.com/hmpl-language/hmpl/blob/main/LICENSE)

---

<div align="center">

💎 **[Star this repo](https://github.com/hmpl-language/hmpl)** • 💻 **[Try HMPL.js](https://hmpl-lang.dev/getting-started)** • 💬 **[Join Discord](https://discord.gg/KFunMep36n)**

<i>This project has been developed with contributions from many amazing community developers!</i>

</div>
