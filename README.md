# Label Printing Microservice

This is a backend **label printing microservice** built with **C# and .NET Core**, using **DDD (Domain-Driven Design)** and **CQRS (Command Query Responsibility Segregation)** architectural patterns. It supports full management of clients, labels, configurations, and printing tasks via Azure message queues.

> 🖥️ **Note:** The **frontend for this application is located in a separate repository**. Please refer to the corresponding UI repository for the Angular application that connects to this backend.

## 🧩 Architecture

- **DDD** – Aggregate roots, entities, value objects, repositories
- **CQRS** – Separation of read and write logic using Commands and Queries
- **Command Bus / Query Bus** – Central dispatching for business actions
- **Validation & Filters** – Ensures data integrity and request correctness
- **SQL Injection Protection** – Secure data access practices

## 🗃️ Databases

- **SQL Server** – Stores reference and supplemental data used to complete labels
- **Azure Cosmos DB** – Stores:
  - Client data
  - Label data
  - Client-label configuration data

## 🛠️ Features

- ➕ Add / ✏️ Edit / ❌ Delete **Clients**
- ➕ Add / ✏️ Edit / ❌ Delete **Labels**
- 🔁 Manage **Label Configurations per Client**
- 📦 Send labels to **Azure Queue** for printing
- 🖨️ Endpoint to **trigger printing** a label
- 🧪 Includes **unit tests** for commands and aggregates

## 🔌 Endpoints

| Method   | Endpoint                                            | Description |
|----------|-----------------------------------------------------|----------------------------------------------|
| `POST`   | `/api/v1/clients/client`                            | Add a new client                             |
| `GET`    | `/api/v1/clients`                                   | Get all clients                              |
| `DELETE` | `/api/v1/clients/{id}`                              | Delete a client                              |
| `GET`    | `/api/v1/labels`                                    | Get all labels                               |
| `POST`   | `/api/v1/labels/label`                              | Add a new label                              |
| `PUT`    | `/api/v1/labels/label`                              | Edit a label                                 |
| `DELETE` | `/api/v1/labels/{id}`                               | Delete a label                               |
| `GET`    | `/api/v1/labels/noconfig/{ClientID}`                | Get all labels without configuration         |
| `POST`   | `/api/v1/labels/configuration/{ClientID}/{LabelID}` | Add a new label configuration for a client   |
| `PUT`    | `/api/v1/labels/configuration/LabelConfiguration`   | Edit an existing configuration               |
| `DELETE` | `/api/v1/labels/configuration/{ClientID}/{LabelID}` | Delete a configuration                       |
| `GET`    | `/api/v1/labels/configuration/{clientId}`           | Get all configurations for a specific client |
| `POST`   | `/api/v1/labels/print`                              | Send a print job to the Azure Queue          |

## ✅ Testing

Includes unit tests for:

- Aggregates
- Command handlers
- Validation logic

Using:

- xUnit
- Moq / Test doubles

## 🔐 Security

- DTO validation
- SQL injection protection via parameterized queries and EF Core best practices
- Input filters and constraints on commands and queries

## ☁️ Azure Integration

- Azure Cosmos DB for client/label/config data
- Azure Queue Storage for print dispatching

## 🔗 Related Repositories

- 🔧 [Frontend Repository – Angular + Angular Material UI](https://github.com/Microrax/MicroLabelsFront)  
  *(Connects to this backend via RESTful API)*

