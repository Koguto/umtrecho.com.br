-------------------------
--CRIA BANCO
-------------------------
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'Loja')
BEGIN
    CREATE DATABASE Loja;
    PRINT 'Banco de dados Loja criado com sucesso.';
END
ELSE
BEGIN
    PRINT 'O banco de dados Loja já existe.';
END
GO

USE [Loja]

GO
/****** Object:  Table [dbo].[Cadastro]    Script Date: 12/03/2025 22:50:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Cadastro]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Cadastro](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[PasswordHash] [nvarchar](255) NULL,
	[Salt] [nvarchar](255) NULL,
	[DataCadastro] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Categorias]    Script Date: 12/03/2025 22:50:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Categorias]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Categorias](
	[CategoriaID] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoriaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Endereco]    Script Date: 12/03/2025 22:50:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Endereco]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Endereco](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PerfilId] [int] NOT NULL,
	[Rua] [nvarchar](200) NULL,
	[Numero] [nvarchar](10) NULL,
	[Complemento] [nvarchar](50) NULL,
	[Bairro] [nvarchar](100) NULL,
	[Cidade] [nvarchar](100) NULL,
	[Estado] [nvarchar](50) NULL,
	[CEP] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Fornecedores]    Script Date: 12/03/2025 22:50:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Fornecedores]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Fornecedores](
	[FornecedorID] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](100) NOT NULL,
	[Telefone] [varchar](20) NULL,
	[Email] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[FornecedorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[MovimentacoesEstoque]    Script Date: 12/03/2025 22:50:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MovimentacoesEstoque]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MovimentacoesEstoque](
	[MovimentacaoID] [int] IDENTITY(1,1) NOT NULL,
	[ProdutoID] [int] NULL,
	[TipoMovimentacao] [char](1) NULL,
	[Quantidade] [int] NOT NULL,
	[DataMovimentacao] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[MovimentacaoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Perfil]    Script Date: 12/03/2025 22:50:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Perfil]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Perfil](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CPF] [nvarchar](450) NULL,
	[Nome] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Produtos]    Script Date: 12/03/2025 22:50:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Produtos]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Produtos](
	[ProdutoID] [int] IDENTITY(1,1) NOT NULL,
	[Codigo] [int] NOT NULL,
	[Nome] [varchar](100) NOT NULL,
	[CategoriaID] [int] NULL,
	[FornecedorID] [int] NULL,
	[Preco] [decimal](10, 2) NOT NULL,
	[QuantidadeEstoque] [int] NULL,
	[ImagemURL] [nvarchar](255) NULL,
	[LinkCompra] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ProdutoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[Cadastro] ON 

INSERT [dbo].[Cadastro] ([ID], [Nome], [Email], [PasswordHash], [Salt], [DataCadastro]) VALUES (1, N'Diego Buchar Montenegro', N'sankzaapp@gmail.com', N'aHtLU7X9aLEFqcKC9gLyW4hQCnSYcDWEno4wf/LEDUk=', N'aHtLU7X9aLEFqcKC9gLyWw==', N'07/02/2025')
SET IDENTITY_INSERT [dbo].[Cadastro] OFF
GO
SET IDENTITY_INSERT [dbo].[Categorias] ON 

INSERT [dbo].[Categorias] ([CategoriaID], [Nome]) VALUES (1, N'Canecas')
INSERT [dbo].[Categorias] ([CategoriaID], [Nome]) VALUES (2, N'Roupas')
INSERT [dbo].[Categorias] ([CategoriaID], [Nome]) VALUES (3, N'Óculos')
INSERT [dbo].[Categorias] ([CategoriaID], [Nome]) VALUES (4, N'Pingentes')
INSERT [dbo].[Categorias] ([CategoriaID], [Nome]) VALUES (5, N'Colares')
SET IDENTITY_INSERT [dbo].[Categorias] OFF
GO
SET IDENTITY_INSERT [dbo].[Endereco] ON 

INSERT [dbo].[Endereco] ([Id], [PerfilId], [Rua], [Numero], [Complemento], [Bairro], [Cidade], [Estado], [CEP]) VALUES (1, 5, N'Rua Engenheiro José Salles', N'200', N'Bl 03 apto 68', N'Interlagos', N'São Paulo', N'SP', N'04776-100')
SET IDENTITY_INSERT [dbo].[Endereco] OFF
GO
SET IDENTITY_INSERT [dbo].[Fornecedores] ON 

INSERT [dbo].[Fornecedores] ([FornecedorID], [Nome], [Telefone], [Email]) VALUES (1, N'Fornecedor EhProibido', N'11-99999-0000', N'contato@ehproibido.com.br')
INSERT [dbo].[Fornecedores] ([FornecedorID], [Nome], [Telefone], [Email]) VALUES (2, N'Fornecedor mofya', N'11-88888-1111', N'reservaink@ehproibido.com.br')
INSERT [dbo].[Fornecedores] ([FornecedorID], [Nome], [Telefone], [Email]) VALUES (3, N'Fornecedor Cigano', N'11-99999-0000', N'contato@ehproibido.com.br')
SET IDENTITY_INSERT [dbo].[Fornecedores] OFF
GO
SET IDENTITY_INSERT [dbo].[MovimentacoesEstoque] ON 

INSERT [dbo].[MovimentacoesEstoque] ([MovimentacaoID], [ProdutoID], [TipoMovimentacao], [Quantidade], [DataMovimentacao]) VALUES (1, 1, N'E', 5, CAST(N'2024-12-27T20:01:27.103' AS DateTime))
SET IDENTITY_INSERT [dbo].[MovimentacoesEstoque] OFF
GO
SET IDENTITY_INSERT [dbo].[Perfil] ON 

INSERT [dbo].[Perfil] ([Id], [CPF], [Nome], [Email]) VALUES (5, NULL, N'Diego Buchar Montenegro', N'sankzaapp@gmail.com')
SET IDENTITY_INSERT [dbo].[Perfil] OFF
GO
SET IDENTITY_INSERT [dbo].[Produtos] ON 

INSERT [dbo].[Produtos] ([ProdutoID], [Codigo], [Nome], [CategoriaID], [FornecedorID], [Preco], [QuantidadeEstoque], [ImagemURL], [LinkCompra]) VALUES (1, 101001, N'Moooo fyaaa!!', 1, 1, CAST(34.50 AS Decimal(10, 2)), 10, N'https://www.mofya.com.br/imagens/moo_fayyy.png', N'https://www.mofya.com.br/produto/')
INSERT [dbo].[Produtos] ([ProdutoID], [Codigo], [Nome], [CategoriaID], [FornecedorID], [Preco], [QuantidadeEstoque], [ImagemURL], [LinkCompra]) VALUES (2, 102001, N'Ganjeiros', 1, 1, CAST(34.50 AS Decimal(10, 2)), 5, N'https://www.mofya.com.br/imagens/ganjeiros.png', N'https://www.mofya.com.br/produto/')
INSERT [dbo].[Produtos] ([ProdutoID], [Codigo], [Nome], [CategoriaID], [FornecedorID], [Preco], [QuantidadeEstoque], [ImagemURL], [LinkCompra]) VALUES (3, 201001, N'Camiseta mofya Liberty', 2, 3, CAST(62.00 AS Decimal(10, 2)), 5, N'https://rsv-ink-images-production.s3.sa-east-1.amazonaws.com/images/product_v2/main_image/1135579d7ae1ce6cfa3a28a61ee30b78.webp', N'https://reserva.ink/mofya/product/camisa-resistencia-reggae')
INSERT [dbo].[Produtos] ([ProdutoID], [Codigo], [Nome], [CategoriaID], [FornecedorID], [Preco], [QuantidadeEstoque], [ImagemURL], [LinkCompra]) VALUES (4, 202001, N'Camiseta mofya Chronics', 2, 3, CAST(80.00 AS Decimal(10, 2)), 5, N'https://rsv-ink-images-production.s3.sa-east-1.amazonaws.com/images/product_v2/main_image/1135579d7ae1ce6cfa3a28a61ee30b78.webp', N'https://reserva.ink/mofya/product/camisa-resistencia-reggae')
INSERT [dbo].[Produtos] ([ProdutoID], [Codigo], [Nome], [CategoriaID], [FornecedorID], [Preco], [QuantidadeEstoque], [ImagemURL], [LinkCompra]) VALUES (5, 203001, N'Camiseta mofya Nature', 2, 3, CAST(80.00 AS Decimal(10, 2)), 5, N'https://rsv-ink-images-production.s3.sa-east-1.amazonaws.com/images/product_v2/main_image/1135579d7ae1ce6cfa3a28a61ee30b78.webp', N'https://reserva.ink/mofya/product/camisa-resistencia-reggae')
INSERT [dbo].[Produtos] ([ProdutoID], [Codigo], [Nome], [CategoriaID], [FornecedorID], [Preco], [QuantidadeEstoque], [ImagemURL], [LinkCompra]) VALUES (6, 301001, N'Óculos Madeira', 3, 3, CAST(150.00 AS Decimal(10, 2)), 5, N'https://rsv-ink-images-production.s3.sa-east-1.amazonaws.com/images/product_v2/main_image/1135579d7ae1ce6cfa3a28a61ee30b78.webp', N'https://reserva.ink/mofya/product/camisa-resistencia-reggae')
INSERT [dbo].[Produtos] ([ProdutoID], [Codigo], [Nome], [CategoriaID], [FornecedorID], [Preco], [QuantidadeEstoque], [ImagemURL], [LinkCompra]) VALUES (7, 302001, N'Óculos Silicone', 3, 3, CAST(200.00 AS Decimal(10, 2)), 5, N'https://rsv-ink-images-production.s3.sa-east-1.amazonaws.com/images/product_v2/main_image/1135579d7ae1ce6cfa3a28a61ee30b78.webp', N'https://reserva.ink/mofya/product/camisa-resistencia-reggae')
INSERT [dbo].[Produtos] ([ProdutoID], [Codigo], [Nome], [CategoriaID], [FornecedorID], [Preco], [QuantidadeEstoque], [ImagemURL], [LinkCompra]) VALUES (8, 403001, N'Pingente Natureza Livre', 4, 3, CAST(40.00 AS Decimal(10, 2)), 5, N'https://rsv-ink-images-production.s3.sa-east-1.amazonaws.com/images/product_v2/main_image/1135579d7ae1ce6cfa3a28a61ee30b78.webp', N'https://reserva.ink/mofya/product/camisa-resistencia-reggae')
INSERT [dbo].[Produtos] ([ProdutoID], [Codigo], [Nome], [CategoriaID], [FornecedorID], [Preco], [QuantidadeEstoque], [ImagemURL], [LinkCompra]) VALUES (9, 404001, N'Pingente Vida Nova', 4, 3, CAST(40.00 AS Decimal(10, 2)), 5, N'https://rsv-ink-images-production.s3.sa-east-1.amazonaws.com/images/product_v2/main_image/1135579d7ae1ce6cfa3a28a61ee30b78.webp', N'https://reserva.ink/mofya/product/camisa-resistencia-reggae')
INSERT [dbo].[Produtos] ([ProdutoID], [Codigo], [Nome], [CategoriaID], [FornecedorID], [Preco], [QuantidadeEstoque], [ImagemURL], [LinkCompra]) VALUES (10, 501001, N'Colar Resina Floral', 5, 3, CAST(60.00 AS Decimal(10, 2)), 5, N'https://rsv-ink-images-production.s3.sa-east-1.amazonaws.com/images/product_v2/main_image/1135579d7ae1ce6cfa3a28a61ee30b78.webp', N'https://reserva.ink/mofya/product/camisa-resistencia-reggae')
SET IDENTITY_INSERT [dbo].[Produtos] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Cadastro__A9D105341731E4D1]    Script Date: 12/03/2025 22:50:56 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Cadastro]') AND name = N'UQ__Cadastro__A9D105341731E4D1')
ALTER TABLE [dbo].[Cadastro] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__Cadastro__DataCa__58520D30]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Cadastro] ADD  DEFAULT (getdate()) FOR [DataCadastro]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__Movimenta__DataM__64B7E415]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MovimentacoesEstoque] ADD  DEFAULT (getdate()) FOR [DataMovimentacao]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__Produtos__Quanti__5EFF0ABF]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Produtos] ADD  DEFAULT ((0)) FOR [QuantidadeEstoque]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Endereco__Perfil__6A70BD6B]') AND parent_object_id = OBJECT_ID(N'[dbo].[Endereco]'))
ALTER TABLE [dbo].[Endereco]  WITH NOCHECK ADD FOREIGN KEY([PerfilId])
REFERENCES [dbo].[Perfil] ([Id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Movimenta__Produ__65AC084E]') AND parent_object_id = OBJECT_ID(N'[dbo].[MovimentacoesEstoque]'))
ALTER TABLE [dbo].[MovimentacoesEstoque]  WITH CHECK ADD FOREIGN KEY([ProdutoID])
REFERENCES [dbo].[Produtos] ([ProdutoID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Produtos__Catego__5FF32EF8]') AND parent_object_id = OBJECT_ID(N'[dbo].[Produtos]'))
ALTER TABLE [dbo].[Produtos]  WITH CHECK ADD FOREIGN KEY([CategoriaID])
REFERENCES [dbo].[Categorias] ([CategoriaID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Produtos__Fornec__60E75331]') AND parent_object_id = OBJECT_ID(N'[dbo].[Produtos]'))
ALTER TABLE [dbo].[Produtos]  WITH CHECK ADD FOREIGN KEY([FornecedorID])
REFERENCES [dbo].[Fornecedores] ([FornecedorID])
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK__Movimenta__TipoM__63C3BFDC]') AND parent_object_id = OBJECT_ID(N'[dbo].[MovimentacoesEstoque]'))
ALTER TABLE [dbo].[MovimentacoesEstoque]  WITH CHECK ADD CHECK  (([TipoMovimentacao]='S' OR [TipoMovimentacao]='E'))
GO


---###########################
--- Criação de usuário
---###########################
CREATE LOGIN usuario_guest WITH PASSWORD = 'Facildemais1234!'
CREATE USER usuario_guest FOR LOGIN usuario_guest;
GRANT ALL PRIVILEGES ON DATABASE::Loja TO usuario_guest;
GRANT SELECT, INSERT ON dbo.Cadastro TO usuario_guest;
GRANT SELECT, INSERT ON dbo.Categorias TO usuario_guest;
GRANT SELECT, INSERT ON dbo.Endereco TO usuario_guest;
GRANT SELECT, INSERT ON dbo.Fornecedores TO usuario_guest;
SELECT name FROM sys.database_principals WHERE type = 'S';
