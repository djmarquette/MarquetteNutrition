RESTORE DATABASE [marquettenutrition] 
FROM  DISK = N'E:\MNF3\SQL\marquettenutrition.bak' 
WITH  FILE = 1,  MOVE N'marquettenutrition_data' 
TO N'c:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\marquettenutrition.mdf',  
MOVE N'marquettenutrition_log' 
TO N'c:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\marquettenutrition_1.ldf',  
NOUNLOAD,  
STATS = 10
GO
