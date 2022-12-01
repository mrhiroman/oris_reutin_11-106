SET IDENTITY_INSERT NftDB.dbo.Users ON
INSERT INTO NftDB.dbo.Users (Id, Login, Email, Salt, HashedPassword, Balance) VALUES (1, N'System', N'system@cryptures.com', N'nothing', N'FIG VZLOMAESH', 9999999);
SET IDENTITY_INSERT NftDB.dbo.Users OFF

SET IDENTITY_INSERT NftDB.dbo.Collections ON
INSERT INTO NftDB.dbo.Collections (Id, Name) VALUES (1, N'None');
INSERT INTO NftDB.dbo.Collections (Id, Name) VALUES (2, N'SellList');
INSERT INTO NftDB.dbo.Collections (Id, Name) VALUES (3, N'Monkeys');
INSERT INTO NftDB.dbo.Collections (Id, Name) VALUES (4, N'Pets');
INSERT INTO NftDB.dbo.Collections (Id, Name) VALUES (5, N'Presidents');
INSERT INTO NftDB.dbo.Collections (Id, Name) VALUES (6, N'CryptoPunks');
SET IDENTITY_INSERT NftDB.dbo.Collections OFF

SET IDENTITY_INSERT NftDB.dbo.Nfts ON

INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (12, N'Monkey 1', 1, N'nfts/monkey1.png', 3);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (13, N'Monkey 2', 1, N'nfts/monkey2.png', 3);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (14, N'Monkey 3', 1, N'nfts/monkey3.png', 3);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (15, N'Monkey 4', 1, N'nfts/monkey4.png', 3);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (16, N'Monkey 5', 1, N'nfts/monkey5.png', 3);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (17, N'Monkey 6', 1, N'nfts/monkey6.png', 3);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (18, N'Monkey 7', 1, N'nfts/monkey7.png', 3);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (19, N'Monkey 8', 1, N'nfts/monkey8.png', 3);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (20, N'Monkey 9', 1, N'nfts/monkey9.png', 3);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (21, N'Monkey 10', 1, N'nfts/monkey10.png', 3);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (22, N'Monkey 11', 1, N'nfts/monkey11.png', 3);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (23, N'Monkey 12', 1, N'nfts/monkey12.png', 3);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (24, N'Pet 1', 1, N'nfts/pet1.png', 4);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (25, N'Pet 2', 1, N'nfts/pet2.png', 4);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (26, N'Pet 3', 1, N'nfts/pet3.png', 4);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (27, N'Pet 4', 1, N'nfts/pet4.png', 4);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (28, N'Pet 5', 1, N'nfts/pet5.png', 4);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (29, N'Pet 6', 1, N'nfts/pet6.png', 4);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (30, N'Pet 7', 1, N'nfts/pet7.png', 4);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (31, N'Pet 8', 1, N'nfts/pet8.png', 4);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (32, N'Pet 9', 1, N'nfts/pet9.png', 4);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (33, N'Pet 10', 1, N'nfts/pet10.png', 4);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (34, N'Pet 11', 1, N'nfts/pet11.png', 4);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (35, N'Pet 12', 1, N'nfts/pet12.png', 4);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (36, N'Pet 13', 1, N'nfts/pet13.png', 4);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (37, N'Pet 14', 1, N'nfts/pet14.png', 4);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (38, N'Pet 15', 1, N'nfts/pet15.png', 4);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (39, N'Pet 16', 1, N'nfts/pet16.png', 4);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (40, N'Benjamin', 1, N'nfts/benjamin.png', 5);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (41, N'Bush', 1, N'nfts/bush.png', 5);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (42, N'Biden', 1, N'nfts/biden.png', 5);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (43, N'Clinton', 1, N'nfts/clinton.png', 5);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (44, N'Kennedy', 1, N'nfts/kennedy.png', 5);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (45, N'Trump', 1, N'nfts/trump.png', 5);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (46, N'Washington', 1, N'nfts/washington.png', 5);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (47, N'Punk 1', 1, N'nfts/punk1.png', 6);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (48, N'Punk 2', 1, N'nfts/punk2.png', 6);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (49, N'Punk 3', 1, N'nfts/punk3.png', 6);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (50, N'Punk 4', 1, N'nfts/punk4.png', 6);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (51, N'Punk 5', 1, N'nfts/punk5.png', 6);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (52, N'Punk 6', 1, N'nfts/punk6.png', 6);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (53, N'Punk 7', 1, N'nfts/punk7.png', 6);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (54, N'Punk 8', 1, N'nfts/punk8.png', 6);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (55, N'Punk 9', 1, N'nfts/punk9.png', 6);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (56, N'Punk 10', 1, N'nfts/punk10.png', 6);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (57, N'Punk 11', 1, N'nfts/punk11.png', 6);
INSERT INTO NftDB.dbo.Nfts (Id, Name, OwnerId, ImagePath, CollectionId) VALUES (58, N'Punk 12', 1, N'nfts/punk12.png', 6);

SET IDENTITY_INSERT NftDB.dbo.Nfts OFF

SET IDENTITY_INSERT NftDB.dbo.Deals ON
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (47, 5, 12, 1, N'active', 3);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (48, 5, 13, 1, N'active', 3);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (49, 5, 14, 1, N'active', 3);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (50, 5, 15, 1, N'active', 3);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (51, 5, 16, 1, N'active', 3);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (52, 5, 17, 1, N'active', 3);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (53, 5, 18, 1, N'active', 3);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (54, 5, 19, 1, N'active', 3);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (55, 5, 20, 1, N'active', 3);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (56, 5, 21, 1, N'active', 3);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (57, 5, 22, 1, N'active', 3);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (58, 5, 23, 1, N'active', 3);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (59, 5, 24, 1, N'active', 4);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (60, 5, 25, 1, N'active', 4);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (61, 5, 26, 1, N'active', 4);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (62, 5, 27, 1, N'active', 4);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (63, 5, 28, 1, N'active', 4);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (64, 5, 29, 1, N'active', 4);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (65, 5, 30, 1, N'active', 4);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (66, 5, 31, 1, N'active', 4);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (67, 5, 32, 1, N'active', 4);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (68, 5, 33, 1, N'active', 4);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (69, 5, 34, 1, N'active', 4);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (70, 5, 35, 1, N'active', 4);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (71, 5, 36, 1, N'active', 4);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (72, 5, 37, 1, N'active', 4);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (73, 5, 38, 1, N'active', 4);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (74, 5, 39, 1, N'active', 4);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (75, 5, 40, 1, N'active', 5);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (76, 5, 41, 1, N'active', 5);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (77, 5, 42, 1, N'active', 5);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (78, 5, 43, 1, N'active', 5);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (79, 5, 44, 1, N'active', 5);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (80, 5, 45, 1, N'active', 5);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (81, 5, 46, 1, N'active', 5);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (82, 5, 47, 1, N'active', 6);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (83, 5, 48, 1, N'active', 6);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (84, 5, 49, 1, N'active', 6);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (85, 5, 50, 1, N'active', 6);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (86, 5, 51, 1, N'active', 6);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (87, 5, 52, 1, N'active', 6);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (88, 5, 53, 1, N'active', 6);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (89, 5, 54, 1, N'active', 6);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (90, 5, 55, 1, N'active', 6);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (91, 5, 56, 1, N'active', 6);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (92, 5, 57, 1, N'active', 6);
INSERT INTO NftDB.dbo.Deals (Id, Cost, NftId, SellerId, Status, CollectionId) VALUES (93, 5, 58, 1, N'active', 6);
SET IDENTITY_INSERT NftDB.dbo.Deals OFF