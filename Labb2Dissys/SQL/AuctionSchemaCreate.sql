CREATE SCHEMA auctionsdb;

CREATE USER 'Labb2Dissys'@'localhost' IDENTIFIED by 'abc.123';

GRANT ALL PRIVILEGES ON auctionsdb.* TO 'Labb2Dissys'@'localhost';

FLUSH PRIVILEGES;