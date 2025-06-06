# News Service

Welcome to the world of **The Wallaby Word**, our quirky news site for Australian audiences. Enjoy!
 
## Setup Documentation

This repo contains a web api application with frontend (Angular, TypeScript, Bootstrap) and backend (C#) folders.

### Tech Stack
- **Frontend:** Angular, TypeScript, Bootstrap
- **Backend:** C# (.NET 9.0)
- **Testing:** XUnit


## Prerequisites

1. **.NET 9.0 SDK** 
2. **Node.js** (version 18+ recommended for Angular, I'm using v22)
3. **npm** (I'm using v11)
4. [**GNews API Key**](https://gnews.io/login)
5. **VS Code** with C# Dev Kit extension (or IDE of your choice)



## Quick Start

1. Install frontend dependencies: `npm install` in `frontend/news-service-frontend`. Start frontend with  `ng serve`
2. Restore backend packages: `dotnet restore` in `backend/src`
3. Configure your GNews API key `dotnet user-secrets set "ExternalApi:ApiKey" "your-api-key-here"`
4. Start backend: `dotnet run` in `backend/src`


## General instructions

### 1. Repository Setup

Pull down repo, using your preferred Git method then open repo in IDE of choice (I typically use VS Code and GitHub Desktop). 

#### GitHub Desktop
1. Go to **Menu > Add > Clone repository > URL**
2. Paste the URL for this repo
3. Clone to your preferred directory



### 2. VS Code Configuration
1. Open two VS Code windows for ease of use (one window for frontend, one window for backend). Open the frontend and backend folders
2. Install the **C# Dev Kit** extension if not already installed


## Frontend Setup

1. Navigate to **frontend/news-service-frontend**
2. Run `npm install` in the terminal to install dependences
  - This should install Angular CLI, Bootstrap and other required packages
3. There is an npm script for running the frontend. Run either the `start` script in **frontend/news-service-frontend/package.json**, or serve the frontend with `ng serve`
4. The frontend will run on [localhost:4200](http://localhost:4200/)
5. Main logic and html for the frontend can be found in **frontend/news-service-frontend/src/app/home** in the home.component.ts and home.component.html files. Main styling can be found in styles.cs



## Backend Setup

1. Navigate to **backend/src**
2. Add your API key as a user secret `dotnet user-secrets init`  `dotnet user-secrets set "ExternalApi:ApiKey" "your-api-key-here"`. The builder will automatically add this to the External Api config when building
4. Run `dotnet restore` command in the terminal to restore the NuGet packages
5. Run `dotnet run`
6. The backend will run on [http://localhost:5056](http://localhost:5056) and [https://localhost:7201](https://localhost:7201). 
7. You can view the data that the backend will return by going to http://localhost:5056/api/articles?language=en
8. Business logic for the backend is in **backend/src/Controllers/ArticlesController.cs**,  **backend/src/Services/ArticleService.cs** and **backend/src/Program.cs** 
   
The frontend will call [http://localhost:5056](http://localhost:5056)


## Testing

### Backend Unit Tests
  
> **Note:** Currently includes one example unit test for demonstration purposes.

### Run the backend test
- Navigate to **backend/tests/backend.Tests**
- Run `dotnet test`. You should see one test that passes
