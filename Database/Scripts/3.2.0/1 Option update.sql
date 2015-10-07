begin transaction
delete from Options where Name = 'FEEDBACK_EMAIL_ADDRESSES'
INSERT INTO [Options] ([Name] ,[Value] ,[Description]) VALUES ('EXEC_EMAIL_CHAIRMAN' ,'nigel.clack@btinternet.com' ,'Chairman''s email address')
INSERT INTO [Options] ([Name] ,[Value] ,[Description]) VALUES ('EXEC_EMAIL_TREASURER' ,'richard@richardbaister.com' ,'Treasurers email address')
INSERT INTO [Options] ([Name] ,[Value] ,[Description]) VALUES ('EXEC_EMAIL_WEBADMIN' ,'philjhale@gmail.com' ,'Web site administrator email address')
INSERT INTO [Options] ([Name] ,[Value] ,[Description]) VALUES ('EXEC_EMAIL_REGISTRAR' ,'richard@richardbaister.com' ,'Registrar''s email address')
INSERT INTO [Options] ([Name] ,[Value] ,[Description]) VALUES ('EXEC_EMAIL_DEVELOPMENT_AND_PRESS_OFFICER' ,'james@middlesbroughlions.com' ,'Development and press officer''s email address')
INSERT INTO [Options] ([Name] ,[Value] ,[Description]) VALUES ('EXEC_EMAIL_FIXURES_AND_REFS_OFFICER' ,'albertlloyd41@gmail.com' ,'Fixtures and refs officer''s email address')

commit