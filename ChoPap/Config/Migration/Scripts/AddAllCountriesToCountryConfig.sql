IF (NOT EXISTS (SELECT * 
                 FROM CountryConfig 
                 WHERE 
				 CountryCode = 'NL' 
				 OR CountryCode = 'FR' 
				 OR CountryCode = 'CA' 
				 OR CountryCode = 'DE' 
				 OR CountryCode = 'BE' 
				 
				 ))
BEGIN
	INSERT INTO CountryConfig (Id, CountryCode, [Name], DoneForTheDay, [Actions], [Wins], [Day])
	VALUES 
	(6, 'DE', 'Germany', 0, 0, 0, 'tisdag'),
	(7, 'CA', 'Canada', 0, 0, 0, 'tisdag'),
	(8, 'FR', 'France', 0, 0, 0, 'tisdag'),
	(9, 'NL', 'Netherlands', 0, 0, 0, 'tisdag'),
	(10, 'BE', 'Belgium', 0, 0, 0, 'tisdag')
END