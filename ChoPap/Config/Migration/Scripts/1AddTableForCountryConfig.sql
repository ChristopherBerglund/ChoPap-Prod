IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_NAME = 'CountryConfig'))
BEGIN
    CREATE TABLE CountryConfig (
        Id int,
        CountryCode varchar(5),
        [Name] varchar(50),
	    DoneForTheDay Bit,
	    [Day] varchar(50)
    );

    INSERT INTO CountryConfig (Id, CountryCode, [Name], DoneForTheDay, [Day])
    VALUES (1, 'SE', 'Sweden', 0, 'söndag');
    INSERT INTO CountryConfig (Id, CountryCode, [Name], DoneForTheDay, [Day])
    VALUES (2, 'DK', 'Denmark', 0, 'söndag');
    INSERT INTO CountryConfig (Id, CountryCode, [Name], DoneForTheDay, [Day])
    VALUES (3, 'NO', 'Norway', 0, 'söndag');
    INSERT INTO CountryConfig (Id, CountryCode, [Name], DoneForTheDay, [Day])
    VALUES (4, 'FI', 'Finland', 0, 'söndag');
    INSERT INTO CountryConfig (Id, CountryCode, [Name], DoneForTheDay, [Day])
    VALUES (5, 'US', 'Usa', 0, 'söndag');
END