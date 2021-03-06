IF db_id('MailingGroup') IS NOT NULL
    DROP DATABASE MailingGroup;
	GO

CREATE DATABASE MailingGroup;
GO

CREATE TABLE MailingGroup.dbo.SystemUser (
	ID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Username NVARCHAR(50)NOT NULL UNIQUE,
	Password NVARCHAR(max) NOT NULL,
	Salt VARBINARY(MAX) NOT NULL
)
GO

CREATE TABLE MailingGroup.dbo.MailingGroup (
	ID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Name NVARCHAR(250) NOT NULL,
	SystemUserID INT NOT NULL,

	FOREIGN KEY (SystemUserID) REFERENCES SystemUser(ID),

	CONSTRAINT Unique_MailingGroup UNIQUE (Name, SystemUserID)
)
GO

CREATE TABLE MailingGroup.dbo.EmailAddress (
	ID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Value NVARCHAR(256) NOT NULL,
	MailingGroupID INT NOT NULL

	FOREIGN KEY(MailingGroupID) REFERENCES MailingGroup(ID),

	CONSTRAINT Unique_EmailAddress UNIQUE (Value, MailingGroupID)
)
GO