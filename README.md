# Allowed Card Operation API

API for checking allowed operations on payment cards.

## Requirements

- .NET 8 SDK
- Visual Studio 2022 or Visual Studio Code

## Running the Application

1. Clone the repository
2. Navigate to the project directory
3. Run the application:
   ```bash
   dotnet run
   ```
4. Open your browser and go to `https://localhost:5001/swagger` to see the API documentation

## Endpoints

### GET /api/card/allowed-actions

Returns a list of allowed actions for a given card.

#### Query Parameters:
- `userId` - user identifier
- `cardNumber` - card number

#### Example Request:
```
GET /api/card/allowed-actions?userId=User1&cardNumber=Card11
```

#### Example Response:
```json
{
  "allowedActions": [
    "ACTION3",
    "ACTION4",
    "ACTION5",
    "ACTION9"
  ]
}
```

## Project Structure

The project follows Clean Architecture principles:

- **Domain**: Contains business entities and interfaces
- **Application**: Contains business logic and use cases
- **Infrastructure**: Contains implementations of interfaces and external services
- **Presentation**: Contains API controllers

## Testing

The project includes unit tests using xUnit. To run the tests:

```bash
dotnet test
``` 