IF (NOT EXISTS (SELECT * 
                 FROM CountryConfig 
                 WHERE CountryCode = 'NL' OR CountryCode = 'FR' OR CountryCode = 'CA' OR CountryCode = 'DE'))
BEGIN
	INSERT INTO CountryConfig (Id, CountryCode, [Name], DoneForTheDay)
	VALUES 
	(6, 'DE', 'Germany', 0),
	(7, 'CA', 'Canada', 0),
	(8, 'FR', 'France', 0),
	(9, 'NL', 'Netherlands', 0);
END