BEGIN
ALTER TABLE NotFoundStocks
ADD OrderId INT NULL, 
	Fixed bit NULL;
END