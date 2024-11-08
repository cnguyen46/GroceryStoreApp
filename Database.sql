use Grocery;
ALTER TABLE [Address] DROP CONSTRAINT FK_Address_UserID;
ALTER TABLE [PaymentMethod] DROP CONSTRAINT FK_PaymentMethod_UserID;
ALTER TABLE [Invoice] DROP CONSTRAINT FK_Invoice_UserID;
ALTER TABLE [Invoice] DROP CONSTRAINT FK_Invoice_AddressID;
ALTER TABLE [Invoice] DROP CONSTRAINT FK_Invoice_PaymentMethodID;
ALTER TABLE [Cart] DROP CONSTRAINT FK_Cart_UserID;
ALTER TABLE [Cart] DROP CONSTRAINT FK_Cart_CouponID;
ALTER TABLE [Cart] DROP CONSTRAINT FK_Cart_InvoiceID;
ALTER TABLE [Coupon] DROP CONSTRAINT FK_Coupon_UserID;
ALTER TABLE [Sale] DROP CONSTRAINT FK_Sale_ProductID;
ALTER TABLE [CartProduct] DROP CONSTRAINT FK_CartProduct_CartID;
ALTER TABLE [CartProduct] DROP CONSTRAINT FK_CartProduct_ProductID;

DROP TABLE IF EXISTS [User];
DROP TABLE IF EXISTS [Address];
DROP TABLE IF EXISTS [PaymentMethod];
DROP TABLE IF EXISTS [Invoice];
DROP TABLE IF EXISTS [Cart];
DROP TABLE IF EXISTS [CartProduct];
DROP TABLE IF EXISTS [Product];
DROP TABLE IF EXISTS [Coupon];
DROP TABLE IF EXISTS [Sale];

CREATE TABLE [User]
(
    [UserID] INT IDENTITY(1,1) PRIMARY KEY,
    [UserName] VARCHAR(50) NOT NULL UNIQUE,
    [FirstName] VARCHAR(50) NOT NULL,
    [LastName] VARCHAR(50) NOT NULL,
    [Email] VARCHAR(50) NOT NULL,
    [Phone] BIGINT NULL,
    [Passcode] VARCHAR(50) NOT NULL
);

CREATE TABLE [Address]
(
    [AddressID] INT IDENTITY(1,1) PRIMARY KEY,
    [UserID] INT NOT NULL,
    [StreetAddress] VARCHAR(100) NOT NULL,
    [State] VARCHAR(20) NOT NULL,
    [City] VARCHAR(30) NOT NULL,
    [Zip] INT NOT NULL
);

CREATE TABLE [PaymentMethod]
(
    [PaymentMethodID] INT IDENTITY(1,1) PRIMARY KEY,
    [UserID] INT NOT NULL,
    [NameOnCard] VARCHAR(50) NOT NULL,
    [CardAccountNumber] VARCHAR(20) NOT NULL,
    [ExpirationDate] DATE NOT NULL,
    [SecurityCode] INT NOT NULL
);

CREATE TABLE [Invoice]
(
    [InvoiceID] INT IDENTITY(1,1) PRIMARY KEY,
    [UserID] INT NOT NULL,
    [ShippingAddressID] INT NOT NULL,
    [PaymentMethodID] INT NOT NULL,
    [CartID] INT NOT NULL,
    [InvoicePrice] DECIMAL(10,2) NOT NULL,
    [InvoiceDate] DATETIME NOT NULL,
);

CREATE TABLE [Cart]
(
    [CartID] INT IDENTITY(1,1) PRIMARY KEY,
    [UserID] INT NOT NULL,
    [CouponID] INT NULL,
    [InvoiceID] INT NULL
);

CREATE TABLE [CartProduct]
(
    [CartID] INT NOT NULL,
    [ProductID] INT NOT NULL,
    [Quantity] INT NOT NULL
);

CREATE TABLE [Product]
(
    [ProductID] INT IDENTITY(1,1) PRIMARY KEY,
    [SKU] VARCHAR(30) NOT NULL,
    [Name] VARCHAR(100) NOT NULL,
    [Description] VARCHAR(1000) NOT NULL,
    [Manufacturer] VARCHAR(50) NOT NULL,
    [Category] VARCHAR(30) NULL,
    [Price] DECIMAL(10,2) NOT NULL,
    [Image] VARCHAR(100) NULL,
    [Size] VARCHAR(50) NULL,
    [Weight] VARCHAR(20) NULL,
    [Rating] DECIMAL(3,1) NULL
);

CREATE TABLE [Coupon]
(
    [CouponID] INT IDENTITY(1,1) PRIMARY KEY,
    [UserID] INT NULL,
    [CouponCode] VARCHAR(20) NOT NULL,
    [AmountOff] DECIMAL(10,2) NULL,
    [PercentageOff] INT NULL,
    [Category] VARCHAR(30) NULL,
    [StartDate] DATETIME NULL,
    [EndDate] DATETIME NULL
);

CREATE TABLE [Sale]
(
    [SaleID] INT IDENTITY(1,1) PRIMARY KEY,
    [ProductID] INT NOT NULL,
    [AmountOff] DECIMAL(10,2) NULL,
    [PercentageOff] INT NULL,
    [StartDate] DATETIME NULL,
    [EndDate] DATETIME NULL
);

ALTER TABLE [Address] ADD CONSTRAINT FK_Address_UserID FOREIGN KEY (UserID) REFERENCES [User](UserID);
ALTER TABLE [PaymentMethod] ADD CONSTRAINT FK_PaymentMethod_UserID FOREIGN KEY (UserID) REFERENCES [User](UserID);
ALTER TABLE [Invoice] ADD CONSTRAINT FK_Invoice_UserID FOREIGN KEY (UserID) REFERENCES [User](UserID);
ALTER TABLE [Invoice] ADD CONSTRAINT FK_Invoice_AddressID FOREIGN KEY (ShippingAddressID) REFERENCES [Address](AddressID);
ALTER TABLE [Invoice] ADD CONSTRAINT FK_Invoice_PaymentMethodID FOREIGN KEY (PaymentMethodID) REFERENCES [PaymentMethod](PaymentMethodID);
ALTER TABLE [Invoice] ADD CONSTRAINT FK_Invoice_CartID FOREIGN KEY (CartID) REFERENCES [Cart](CartID);
ALTER TABLE [Cart] ADD CONSTRAINT FK_Cart_UserID FOREIGN KEY (UserID) REFERENCES [User](UserID);
ALTER TABLE [Cart] ADD CONSTRAINT FK_Cart_CouponID FOREIGN KEY (CouponID) REFERENCES [Coupon](CouponID);
ALTER TABLE [Cart] ADD CONSTRAINT FK_Cart_InvoiceID FOREIGN KEY (InvoiceID) REFERENCES [Invoice](InvoiceID);
ALTER TABLE [Coupon] ADD CONSTRAINT FK_Coupon_UserID FOREIGN KEY (UserID) REFERENCES [User](UserID);
ALTER TABLE [Sale] ADD CONSTRAINT FK_Sale_ProductID FOREIGN KEY (ProductID) REFERENCES [Product](ProductID);
ALTER TABLE [CartProduct] ADD CONSTRAINT FK_CartProduct_CartID FOREIGN KEY (CartID) REFERENCES [Cart](CartID);
ALTER TABLE [CartProduct] ADD CONSTRAINT FK_CartProduct_ProductID FOREIGN KEY (ProductID) REFERENCES [Product](ProductID);

SET IDENTITY_INSERT [User] ON;
INSERT INTO [User] (UserID, UserName, FirstName, LastName, Email, Phone, Passcode)
VALUES    (1,'Eldin.salja', 'Eldin', 'Salja', 'eldin.da.man@gmail.com', 4021234567, 'Passwordmine1'),
    (2, 'da.chief', 'Chief', 'Keef', 'keefin@hotmail.com', 5317654321, 'keeponkeefinon'),
	(3, 'testUser', 'Chief', 'Keef', 'keefin@hotmail.com', 5317654321, 'Testing123');
SET IDENTITY_INSERT [User] OFF;

SET IDENTITY_INSERT [Address] ON;
INSERT INTO [Address] (AddressID, UserID, StreetAddress, State, City, Zip)
VALUES	  (1, 1, '330 El Chapo Ln.', 'NE', 'Lincoln', 68521),
    (2, 2, '420 Blaze Dr.', 'NE', 'Omaha', 68506);
SET IDENTITY_INSERT [Address] OFF;

SET IDENTITY_INSERT [PaymentMethod] ON;
INSERT INTO [PaymentMethod] (PaymentMethodID, UserID, NameOnCard, CardAccountNumber, ExpirationDate, SecurityCode)
VALUES    (1, 1, 'Eldin Salja', '1234567890123456', '10/12/2024', 333),
    (2, 2, 'Chief Keef', '4444555566667777', '7/6/2024', 456);
SET IDENTITY_INSERT [PaymentMethod] OFF;

SET IDENTITY_INSERT [Cart] ON;
INSERT INTO [Cart] (CartID, UserID, CouponID, InvoiceID)
VALUES    (1, 1, NULL, Null);
SET IDENTITY_INSERT [Cart] OFF;

SET IDENTITY_INSERT [Invoice] ON;
INSERT INTO [Invoice] (InvoiceID, UserID, ShippingAddressID, PaymentMethodID, CartID, InvoicePrice, InvoiceDate)
VALUES    (1, 2, 2, 2, 1, 420, '4/4/2024');
SET IDENTITY_INSERT [Invoice] OFF;

UPDATE [Cart] SET InvoiceID = 1;

SET IDENTITY_INSERT [Product] ON;
INSERT INTO [Product] (ProductID, SKU, Name, Description, Manufacturer, Category, Price, Image, Size, Weight, Rating)
VALUES    (1, '54861385315', 'Apple', 'Red Apple', 'Sun Orchard', 'Fruit', 2.29, '/images/fruit/apple.jfif', '4x4x4 in', '.5 lbs', 5),
    (2, '431835165135', 'Banana', 'Yellow Banana', 'Dole', 'Fruit', 3.19, '/images/fruit/banana.jfif', '8x2x2 in', '.2 lbs', 4.8),
    (3, '3548318355', 'Grape', 'Purple Grapes', 'Grapeman Farms', 'Fruit', 5.14, '/images/fruit/grape.jpeg', '1x1x1 in', '.02 lbs', 3.9),
	(4, '54861385315', 'Spinach', 'Fresh Spinach', 'Green Farms', 'Vegetable', 1.99, '/images/vegetables/spinach.jfif', '10x8x2 in', '0.3 lbs', 4.7),
	(5, '431835165135', 'Carrot', 'Organic Carrot', 'Harvest Farms', 'Vegetable', 2.49, '/images/vegetables/carrot.jfif', '6x2x2 in', '0.25 lbs', 4.5),
	(6, '3548318355', 'Tomato', 'Red Tomato', 'Sunrise Gardens', 'Vegetable', 0.99, '/images/vegetables/tomato.jfif', '3x3x3 in', '0.1 lbs', 4.8),
	(7, '54861385315', 'Orange Juice', 'Fresh Orange Juice', 'Tropicana', 'Beverage', 3.99, '/images/beverages/orange_juice.jfif', '6x3x3 in', '1.5 lbs', 4.6),
	(8, '431835165135', 'Green Tea', 'Organic Green Tea', 'Arizona', 'Beverage', 4.29, '/images/beverages/green_tea.jfif', '4x4x6 in', '0.8 lbs', 4.9),
	(9, '3548318355', 'Coffee', 'Premium Coffee Beans', 'Morning Brew', 'Beverage', 8.99, '/images/beverages/coffee.jfif', '5x5x8 in', '1.2 lbs', 4.7),
	(10, '54861385315', 'Frozen Pizza', 'Supreme Pizza', 'Digiorno', 'Frozen Food', 6.99, '/images/frozen/pizza.jfif', '12x12x2 in', '1.8 lbs', 4.5),
	(11, '431835165135', 'Frozen Berries', 'Mixed Berries', 'Dole', 'Frozen Food', 5.49, '/images/frozen/berries.png', '8x4x2 in', '0.6 lbs', 4.8),
	(12, '3548318355', 'Frozen Dumplings', 'Pork Dumplings', 'Gyoza', 'Frozen Food', 7.99, '/images/frozen/dumplings.jfif', '6x4x1 in', '0.5 lbs', 4.6);
SET IDENTITY_INSERT [Product] OFF;

INSERT INTO [CartProduct] (CartID, ProductID, Quantity)
VALUES    (1, 1, 7),
    (1, 2, 1),
    (1, 3, 1),
	(1, 7, 4),
    (1, 12, 1),
    (1, 9, 3);

SET IDENTITY_INSERT [Coupon] ON;
INSERT INTO [Coupon] (CouponID, UserID, CouponCode, AmountOff, PercentageOff, Category, StartDate, EndDate)
VALUES    (1, NULL, '5-off', 5.00, NULL, 'Fruit', NULL, NULL);
SET IDENTITY_INSERT [Coupon] OFF;

SET IDENTITY_INSERT [Sale] ON;
INSERT INTO [Sale] (SaleID, ProductID, AmountOff, PercentageOff, StartDate, EndDate)
VALUES    (1, 1, NULL, 5, NULL, NULL);
SET IDENTITY_INSERT [Sale] OFF;