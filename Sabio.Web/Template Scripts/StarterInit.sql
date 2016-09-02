

/****** Object:  UserDefinedTableType [dbo].[IntIdTable]    Script Date: 1/23/2015 12:11:33 AM ******/
CREATE TYPE [dbo].[IntIdTable] AS TABLE(
	[Data] [int] NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[Data] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)




/****** Object:  UserDefinedTableType [dbo].[NVarCharTable]    Script Date: 1/23/2015 12:11:41 AM ******/
CREATE TYPE [dbo].[NVarCharIdTable] AS TABLE(
	[Data] [varchar](128) NOT NULL
	PRIMARY KEY CLUSTERED 
(
	[Data] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)




/****** Object:  UserDefinedTableType [dbo].[NVarCharTable]    Script Date: 1/23/2015 12:12:35 AM ******/
CREATE TYPE [dbo].[NVarCharTable] AS TABLE(
	[Data] [nvarchar](max) NOT NULL
)
GO



/****** Object:  UserDefinedTableType [dbo].[UniqueIdTable]    Script Date: 1/23/2015 12:12:17 AM ******/
CREATE TYPE [dbo].[UniqueIdTable] AS TABLE(
	[Data] [uniqueidentifier] NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[Data] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
GO


/*

Generate Data

INSERT INTO [dbo].[TestTableOne]
           ([UId]
           ,[FirstName]
           ,[LastName]
           ,[Email]
           
           ,[Age])
   SELECT TOP 100 
				newid()
			 
			  ,[FirstName]
			  ,[LastName]
			  ,null
			  , 53
  FROM [C04].[dbo].[ExternalVoterFileData]
  Order by NewID()



*/
