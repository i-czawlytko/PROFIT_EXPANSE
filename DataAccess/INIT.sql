IF OBJECT_ID('DBO.ROLE') IS NULL BEGIN
	CREATE TABLE [ROLE]
	(
		[Id] INT PRIMARY KEY,
		[Name] NVARCHAR(100)
	);
	INSERT INTO [ROLE] VALUES (1,N'Администратор'), (2,N'Пользователь');
END

IF OBJECT_ID('DBO.USER') IS NULL BEGIN
	CREATE TABLE [USER]
	(
		[Id] INT IDENTITY(1,1) PRIMARY KEY,
		[Login] NVARCHAR(100) NOT NULL UNIQUE CHECK([Login] !=''),
		[FullName] NVARCHAR(100) NOT NULL CHECK([FullName] !=''),
		[RoleId] INT NOT NULL,
		[Inserted] DateTime NOT NULL,
		CONSTRAINT FK_USER_TO_ROLE FOREIGN KEY (RoleId)  REFERENCES [ROLE] (Id)
	)
END


IF OBJECT_ID('DBO.CATEGORY') IS NULL BEGIN
	CREATE TABLE [CATEGORY]
	(
		[Id] INT IDENTITY(1,1) PRIMARY KEY,
		[Name] NVARCHAR(100) NOT NULL UNIQUE CHECK([Name] !='')
	)
END

IF OBJECT_ID('DBO.RECIPIENT') IS NULL BEGIN
	CREATE TABLE [RECIPIENT]
	(
		[Id] INT IDENTITY(1,1) PRIMARY KEY,
		[Name] NVARCHAR(100) NOT NULL UNIQUE CHECK([Name] !='')
	)
END

IF OBJECT_ID('DBO.DATA') IS NULL BEGIN
	CREATE TABLE [DATA]
	(
		[Id] INT IDENTITY(1,1) PRIMARY KEY,
		[Summ] INT NOT NULL CHECK(Summ > 0),
		[Description] NVARCHAR(100),
		[CategoryId] INT NOT NULL,
		[RecipientId] INT NOT NULL,
		[OperationType] TINYINT NOT NULL,
		[Inserted] DateTime NOT NULL,
		CONSTRAINT FK_DATA_TO_CATEGORY FOREIGN KEY (CategoryId)  REFERENCES [CATEGORY] (Id),
		CONSTRAINT FK_DATA_TO_RECIPIENT FOREIGN KEY (RecipientId)  REFERENCES [RECIPIENT] (Id)
	)
END
