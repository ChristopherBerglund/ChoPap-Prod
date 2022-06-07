IF NOT EXISTS(SELECT *
          FROM   INFORMATION_SCHEMA.COLUMNS
          WHERE  TABLE_NAME = 'CountryConfig'
                 AND COLUMN_NAME = 'Actions') 
BEGIN
	ALTER TABLE CountryConfig
	ADD [Actions] int, [Wins] int
END