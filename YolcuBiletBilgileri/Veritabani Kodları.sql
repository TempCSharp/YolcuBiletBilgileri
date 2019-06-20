CREATE TABLE [dbo].[Bilet] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [SeferId]  INT           NOT NULL,
    [Tc]       NVARCHAR (50) NOT NULL,
    [Adi]      NVARCHAR (50) NOT NULL,
    [Soyadi]   NVARCHAR (50) NOT NULL,
    [Yas]      NVARCHAR (50) NOT NULL,
    [Telefon]  NVARCHAR (50) NOT NULL,
    [Cinsiyet] NVARCHAR (50) NOT NULL,
    [KoltukNo] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Bilet_ToTable] FOREIGN KEY ([SeferId]) REFERENCES [dbo].[Sefer] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE [dbo].[Kullanici] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [KullaniciAdi] NVARCHAR (50) NOT NULL,
    [Sifre]        NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Sefer] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [AracAdi]  NVARCHAR (50) NOT NULL,
    [Nereden]  NVARCHAR (50) NOT NULL,
    [Nereye]   NVARCHAR (50) NOT NULL,
    [Saat]     NVARCHAR (50) NOT NULL,
    [Tarih]    NVARCHAR (50) NOT NULL,
    [AracTuru] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);