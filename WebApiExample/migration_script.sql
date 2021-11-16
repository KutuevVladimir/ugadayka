CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    PRIMARY KEY (`MigrationId`)
);

START TRANSACTION;

CREATE TABLE `Tracks` (
    `TrackId` int NOT NULL AUTO_INCREMENT,
    `Name` text NULL,
    `Author` text NULL,
    `Url` text NULL,
    PRIMARY KEY (`TrackId`)
);

CREATE TABLE `Playlists` (
    `PlaylistId` int NOT NULL AUTO_INCREMENT,
    `Name` text NULL,
    `Image` varbinary(4000) NULL,
    `PlayerId` varchar(767) NULL,
    PRIMARY KEY (`PlaylistId`)
);

CREATE TABLE `PlaylistsToTracks` (
    `PlaylistId` int NOT NULL,
    `TrackId` int NOT NULL,
    PRIMARY KEY (`TrackId`, `PlaylistId`),
    CONSTRAINT `FK_PlaylistsToTracks_Playlists_PlaylistId` FOREIGN KEY (`PlaylistId`) REFERENCES `Playlists` (`PlaylistId`) ON DELETE CASCADE,
    CONSTRAINT `FK_PlaylistsToTracks_Tracks_TrackId` FOREIGN KEY (`TrackId`) REFERENCES `Tracks` (`TrackId`) ON DELETE CASCADE
);

CREATE TABLE `PlaylistsToLikes` (
    `PlaylistId` int NOT NULL,
    `PlayerId` int NOT NULL,
    `PlayerId1` varchar(767) NULL,
    PRIMARY KEY (`PlayerId`, `PlaylistId`),
    CONSTRAINT `FK_PlaylistsToLikes_Playlists_PlaylistId` FOREIGN KEY (`PlaylistId`) REFERENCES `Playlists` (`PlaylistId`) ON DELETE CASCADE
);

CREATE TABLE `Rooms` (
    `RoomId` int NOT NULL AUTO_INCREMENT,
    `Name` text NULL,
    `Image` varbinary(4000) NULL,
    `State` tinyint(1) NOT NULL,
    `MaxPlayers` int NOT NULL,
    `RequiresPassword` tinyint(1) NOT NULL,
    `Password` text NULL,
    `PlayerId` varchar(767) NULL,
    `PlaylistId` int NULL,
    PRIMARY KEY (`RoomId`),
    CONSTRAINT `FK_Rooms_Playlists_PlaylistId` FOREIGN KEY (`PlaylistId`) REFERENCES `Playlists` (`PlaylistId`) ON DELETE RESTRICT
);

CREATE TABLE `Players` (
    `PlayerId` varchar(767) NOT NULL,
    `DisplayName` text NULL,
    `Email` text NULL,
    `Rating` int NOT NULL,
    `Image` text NULL,
    `is_admin` tinyint(1) NOT NULL,
    `RoomId` int NULL,
    PRIMARY KEY (`PlayerId`),
    CONSTRAINT `FK_Players_Rooms_RoomId` FOREIGN KEY (`RoomId`) REFERENCES `Rooms` (`RoomId`) ON DELETE RESTRICT
);

CREATE INDEX `IX_Players_RoomId` ON `Players` (`RoomId`);

CREATE INDEX `IX_Playlists_PlayerId` ON `Playlists` (`PlayerId`);

CREATE INDEX `IX_PlaylistsToLikes_PlayerId1` ON `PlaylistsToLikes` (`PlayerId1`);

CREATE INDEX `IX_PlaylistsToLikes_PlaylistId` ON `PlaylistsToLikes` (`PlaylistId`);

CREATE INDEX `IX_PlaylistsToTracks_PlaylistId` ON `PlaylistsToTracks` (`PlaylistId`);

CREATE INDEX `IX_Rooms_PlayerId` ON `Rooms` (`PlayerId`);

CREATE INDEX `IX_Rooms_PlaylistId` ON `Rooms` (`PlaylistId`);

ALTER TABLE `Playlists` ADD CONSTRAINT `FK_Playlists_Players_PlayerId` FOREIGN KEY (`PlayerId`) REFERENCES `Players` (`PlayerId`) ON DELETE RESTRICT;

ALTER TABLE `PlaylistsToLikes` ADD CONSTRAINT `FK_PlaylistsToLikes_Players_PlayerId1` FOREIGN KEY (`PlayerId1`) REFERENCES `Players` (`PlayerId`) ON DELETE RESTRICT;

ALTER TABLE `Rooms` ADD CONSTRAINT `FK_Rooms_Players_PlayerId` FOREIGN KEY (`PlayerId`) REFERENCES `Players` (`PlayerId`) ON DELETE RESTRICT;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20211109133607_InitialCreate', '5.0.11');

COMMIT;

