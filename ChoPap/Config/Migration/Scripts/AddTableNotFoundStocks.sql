﻿IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_NAME = 'NotFoundStocks'))
BEGIN

	CREATE TABLE NotFoundStocks (
		[Name] varchar(100),
		CountryCode varchar(3),
		[Date] datetime2(7)
	);
END