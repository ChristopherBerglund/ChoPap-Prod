﻿--IF (NOT EXISTS (SELECT * 
--                 FROM INFORMATION_SCHEMA.TABLES 
--                 WHERE TABLE_NAME = 'Personsens'))
--BEGIN
--CREATE TABLE Personsens (
--    PersonensID int
--);
--END