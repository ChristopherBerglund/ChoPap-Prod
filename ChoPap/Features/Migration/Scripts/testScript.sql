﻿--IF (NOT EXISTS (SELECT * 
--                 FROM INFORMATION_SCHEMA.TABLES 
--                 WHERE TABLE_NAME = 'Persons'))
--BEGIN
--CREATE TABLE Persons (
--    PersonID int
--);
--END