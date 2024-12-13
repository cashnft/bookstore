-- init-scripts/01-init.sql
-- Note: Modified from previous SQL to be PostgreSQL compatible

CREATE TABLE Authors (
    AuthorId SERIAL PRIMARY KEY,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Biography TEXT,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMP
);

CREATE TABLE Books (
    BookId SERIAL PRIMARY KEY,
    ISBN VARCHAR(13) UNIQUE NOT NULL,
    Title VARCHAR(200) NOT NULL,
    Description TEXT,
    Price DECIMAL(10, 2) NOT NULL,
    PublicationDate DATE NOT NULL,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMP
);

CREATE TABLE BookAuthors (
    BookId INTEGER,
    AuthorId INTEGER,
    PRIMARY KEY (BookId, AuthorId),
    FOREIGN KEY (BookId) REFERENCES Books(BookId),
    FOREIGN KEY (AuthorId) REFERENCES Authors(AuthorId)
);

CREATE TABLE Customers (
    CustomerId SERIAL PRIMARY KEY,
    Email VARCHAR(100) UNIQUE NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    PhoneNumber VARCHAR(20),
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMP
);

CREATE TABLE Addresses (
    AddressId SERIAL PRIMARY KEY,
    CustomerId INTEGER,
    AddressLine1 VARCHAR(100) NOT NULL,
    AddressLine2 VARCHAR(100),
    City VARCHAR(50) NOT NULL,
    State VARCHAR(50),
    PostalCode VARCHAR(20) NOT NULL,
    Country VARCHAR(50) NOT NULL,
    IsDefault BOOLEAN DEFAULT FALSE,
    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
);

CREATE TABLE Inventory (
    BookId INTEGER PRIMARY KEY,
    StockLevel INTEGER NOT NULL DEFAULT 0,
    LastRestocked TIMESTAMP,
    FOREIGN KEY (BookId) REFERENCES Books(BookId)
);

CREATE TABLE Orders (
    OrderId SERIAL PRIMARY KEY,
    CustomerId INTEGER NOT NULL,
    OrderDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    TotalAmount DECIMAL(10, 2) NOT NULL,
    Status VARCHAR(20) NOT NULL DEFAULT 'Pending',
    ShippingAddressId INTEGER NOT NULL,
    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId),
    FOREIGN KEY (ShippingAddressId) REFERENCES Addresses(AddressId)
);

CREATE TABLE OrderItems (
    OrderId INTEGER,
    BookId INTEGER,
    Quantity INTEGER NOT NULL,
    PriceAtTime DECIMAL(10, 2) NOT NULL,
    PRIMARY KEY (OrderId, BookId),
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId),
    FOREIGN KEY (BookId) REFERENCES Books(BookId)
);

-- Create indexes
CREATE INDEX IX_Books_ISBN ON Books(ISBN);
CREATE INDEX IX_Customers_Email ON Customers(Email);
CREATE INDEX IX_Orders_CustomerId ON Orders(CustomerId);
CREATE INDEX IX_Orders_Status ON Orders(Status);