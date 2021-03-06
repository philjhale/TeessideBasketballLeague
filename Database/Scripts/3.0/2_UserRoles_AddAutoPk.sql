/*
   06 July 201100:39:59
   User: 
   Server: Dell-PC\SQLEXPRESS
   Database: LiveBackup
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_UserRoles
	(
	Id int NOT NULL IDENTITY (1, 1),
	User_Id int NOT NULL,
	Role_Id int NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_UserRoles SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_UserRoles OFF
GO
IF EXISTS(SELECT * FROM dbo.UserRoles)
	 EXEC('INSERT INTO dbo.Tmp_UserRoles (User_Id, Role_Id)
		SELECT User_Id, Role_Id FROM dbo.UserRoles WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.UserRoles
GO
EXECUTE sp_rename N'dbo.Tmp_UserRoles', N'UserRoles', 'OBJECT' 
GO
ALTER TABLE dbo.UserRoles ADD CONSTRAINT
	PK_UserRoles PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
select Has_Perms_By_Name(N'dbo.UserRoles', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.UserRoles', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.UserRoles', 'Object', 'CONTROL') as Contr_Per 