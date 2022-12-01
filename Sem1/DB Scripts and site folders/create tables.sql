create table Users
(
    Id             int identity
        primary key,
    Login          varchar(50)  not null
        unique,
    Email          varchar(50)  not null
        unique,
    Salt           varchar(64)  not null,
    HashedPassword varchar(100) not null,
    Balance        int
        check ([Balance] >= 0)
)
go



create table Collections
(
    Id   int identity
        primary key,
    Name varchar(30) not null
)
go




create table Nfts
(
    Id           int identity
        primary key,
    Name         varchar(30) not null,
    OwnerId      int         not null
        references Users,
    ImagePath    varchar(50),
    CollectionId int
        references Collections
)
go

create table Deals
(
    Id           int identity
        primary key,
    Cost         int not null
        check ([Cost] > 0),
    NftId        int not null
        references Nfts,
    SellerId     int not null
        references Users,
    Status       varchar(6),
    CollectionId int not null
        references Collections
)
go


create table Wallets
(
    UserId  int not null
        primary key
        references Users,
    Binance varchar(64),
    Bybit   varchar(64),
    Bitcoin varchar(64)
)
go

