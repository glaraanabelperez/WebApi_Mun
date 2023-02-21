EXEC ('sp_fulltext_database enable');
CREATE FULLTEXT CATALOG fulltextCatalog AS DEFAULT;   
DROP FULLTEXT INDEX ON [MundoPanal2].[dbo].[Products];

CREATE FULLTEXT INDEX ON [MundoPanal2].[dbo].[Products]([Name]) 
KEY INDEX PK_Products_NameFullText
WITH STOPLIST = SYSTEM;


CREATE UNIQUE INDEX PK_Product_FullText ON [MundoPanal2].[dbo].[Products]([ProductId]);  
CREATE FULLTEXT CATALOG ft AS DEFAULT;  
CREATE FULLTEXT INDEX ON [MundoPanal2].[dbo].[Products]([Name])   
   KEY INDEX PK_Product_FullText   
   WITH STOPLIST = SYSTEM;  
GO 