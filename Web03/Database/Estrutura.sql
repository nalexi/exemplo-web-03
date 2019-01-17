CREATE TABLE categorias (
	id int PRIMARY KEY IDENTITY(1,1),
	nome VARCHAR(100),
	registro_ativo bit

);

CREATE TABLE cores (
	id int PRIMARY KEY IDENTITY(1,1),
	nome VARCHAR(100),
	registro_ativo bit

);

CREATE TABLE brinquedos(
	id INT PRIMARY KEY IDENTITY(1,1),
	nome VARCHAR(100),
	marca VARCHAR(100),
	preco DECIMAL,
	estoque SMALLINT,
	registro_ativo bit,

);

CREATE TABLE carros(
	id INT PRIMARY KEY IDENTITY(1,1),
	modelo VARCHAR(100),
	preco INT,
	data_compra DATETIME2,
	registro_ativo bit,	

);
