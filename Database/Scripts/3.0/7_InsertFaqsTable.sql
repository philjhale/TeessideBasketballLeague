/*
   17 July 201111:05:46
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
CREATE TABLE dbo.Faqs
	(
	Id int NOT NULL IDENTITY (1, 1),
	Title varchar(300) NOT NULL,
	Text varchar(3800) NOT NULL,
	LastUpdated datetime NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Faqs ADD CONSTRAINT
	PK_Faqs PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Faqs SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Faqs', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Faqs', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Faqs', 'Object', 'CONTROL') as Contr_Per 