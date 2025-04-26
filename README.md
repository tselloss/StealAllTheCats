# StealAllTheCats

Welcome to **StealAllTheCats**, a fun project that sets up a .NET API connected to a SQL Server database running inside Docker!

This README will guide you step-by-step to set it up and run the API locally.

---

## Requirements

- .NET 8 SDK
- Docker and Docker Compose
- Git (optional, for cloning the project)
- Visual Studio, Rider, VS Code, or any IDE you like

---

## Steps to Run

### 1. Clone the repository

```bash
 git clone https://github.com/your-username/StealAllTheCats.git
 cd StealAllTheCats
```
#### Using LocalDB

If you prefer not to use Docker, you can set up the database using **LocalDB** (comes with Visual Studio).

1. Open **SQL Server Management Studio (SSMS)** or **SQL Server Object Explorer** in Visual Studio.
2. Connect to **(localdb)\\MSSQLLocalDB**.
3. Manually create a database called `StealAllTheCatsDb`.
4. Update your `appsettings.json` connection string to:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=StealAllTheCatsDb;Trusted_Connection=True;"
  }
}
```


###  Set up the Database using Docker

We use Docker to spin up a SQL Server instance.

If you don't have Docker Compose yet, install it first.

**Run this command:**

```bash
 docker-compose up -d
```

This will:

- Pull the official Microsoft SQL Server image
- Create a database container
- Expose it on port **1433**

> Make sure port 1433 is not blocked by your firewall!

---

###  Configure the Connection String for Docker

In `appsettings.json`, ensure your connection string matches:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=StealAllTheCatsDb;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;"
  }
}
```

## Run the API

If you're using Visual Studio:

1. In the toolbar next to the **Run** button (the green arrow ▶️), there’s a **dropdown**.
2. Select whether you want to run:
   - **Docker**: Runs your project inside a container.
   - **ProjectName (HTTPS)**: Runs locally on your machine (without Docker).

> ✅ For first-time setup, choose **Docker** to make sure the database container and API container work together.

> 🛠️ Later, for quicker testing, you can also run the project directly (HTTPS) if the database container is already up.

When running with Docker, Visual Studio will automatically expose your app at something like:

- [**https://localhost:11906/swagger**](https://localhost:11906/swagger)

Or if running locally (without Docker):

- [**https://localhost:5001**](https://localhost:5001)
- [**http://localhost:5000**](http://localhost:5000)


---


