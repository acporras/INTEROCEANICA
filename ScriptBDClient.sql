USE RSFACCAR
GO

ALTER DATABASE RSFACCAR
SET COMPATIBILITY_LEVEL = 130 -- For SQL Server 2016
GO

--NORMALIZANDO LA BASE DE LA EMPRESA UNO
ALTER TABLE FT0001FACC
	ADD F5_COD_ESTADO_SUNAT INT
ALTER TABLE FT0001FACC
	ADD F5_MENSAJE_SUNAT VARCHAR(500)
ALTER TABLE FT0001FACC
	ADD F5_ESTADO_ENVIO INT
ALTER TABLE FT0001FACC
	ADD F5_XML VARCHAR(250)
ALTER TABLE FT0001FACC
	ADD F5_CDR VARCHAR(250)
ALTER TABLE FT0001FACC
	ADD F5_PDF VARCHAR(250)
GO

CREATE PROCEDURE SPS_TABFACCAB_BY_ESTDOCELE(
	@TX_ESTDOCELE VARCHAR(150),
	@NO_DOCELECAB VARCHAR(50)
)
AS
BEGIN
	DECLARE @TBL_DOCELECAB NVARCHAR(MAX);
	SET @TBL_DOCELECAB = 'SELECT * FROM ' + @NO_DOCELECAB + ' WHERE F5_ESTADO_ENVIO IN(
		SELECT value  
		FROM STRING_SPLIT(''' + @TX_ESTDOCELE + ''', '','')  
		WHERE RTRIM(value) <> ''''
	)'
	EXEC SP_EXECUTESQL @TBL_DOCELECAB
END
GO

CREATE PROCEDURE SPS_TABFACDET_BY_TABFACCAB(
	@CO_DETALTIDO CHAR(2),
	@NU_DETSERSUN CHAR(4),
	@NU_DETNUMSUN CHAR(7),
	@NO_DOCELEDET VARCHAR(50)
)
AS
BEGIN
	DECLARE @TBL_DOCELEDET NVARCHAR(MAX);
	SET @TBL_DOCELEDET = 'SELECT * FROM ' + @NO_DOCELEDET +
	' WHERE F6_CTD = ' + @CO_DETALTIDO +
	'AND F6_CNUMSER = ' + @NU_DETSERSUN +
	'AND F6_CNUMDOC = ' + @NU_DETNUMSUN

	EXEC SP_EXECUTESQL @TBL_DOCELEDET
END
GO