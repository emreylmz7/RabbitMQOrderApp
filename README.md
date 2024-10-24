# Order Processing Microservice with RabbitMQ

## Project Description
This project simulates an order processing system using RabbitMQ within a microservices architecture. Users can create an order via an API, which is then sent to a RabbitMQ queue. The order is picked up and processed by another microservice.

## Technologies
- **.NET Core**: For developing the Web API.
- **RabbitMQ**: Messaging broker.
- **C#**: Programming language.
- **Docker**: To run RabbitMQ (optional).

### Usage
- When an order is sent to the OrderAPI service, it is added to the RabbitMQ queue.
- The OrderProcessingService listens to the queue to receive and process the orders.

## Project Structure
- **OrderAPI**: Service for creating orders.
- **OrderProcessingService**: Reads and processes orders from the RabbitMQ queue.

## Installation

### Prerequisites
- .NET SDK (version 5.0 or later)
- RabbitMQ (locally or via Docker)

