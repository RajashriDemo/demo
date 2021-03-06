CREATE DATABASE ShopBridge


CREATE TABLE [dbo].[ItemMst](
	[ItemId] [int] IDENTITY(1,1) NOT NULL,
	[ItemName] [nvarchar](50) NULL,
	[ItemDescription] [nvarchar](1000) NULL,
	[ItemPrice] [decimal](10, 2) NULL,
	[ItemImage] [nvarchar](200) NULL,
 CONSTRAINT [PK_ItemMst] PRIMARY KEY CLUSTERED 
(
	[ItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]