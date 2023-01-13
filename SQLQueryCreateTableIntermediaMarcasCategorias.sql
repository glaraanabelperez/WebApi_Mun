
CREATE TABLE CategoryMarcas (
    CategoryMarcaId int NOT NULL IDENTITY(1,1),
    CategoryId int NOT NULL,
    MarcaId int   NOT NULL,
	[State] tinyint not null,
    PRIMARY KEY (CategoryMarcaId),
	FOREIGN KEY (CategoryId) REFERENCES Categories([CategoryId]),
    FOREIGN KEY (MarcaId) REFERENCES Marcas(MarcaId)
);



   


