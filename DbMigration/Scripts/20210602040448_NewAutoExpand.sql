﻿IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210602040448_NewAutoExpand')

BEGIN

    ALTER TABLE Item

    ADD AutoExpand bit

    DEFAULT 0 NOT NULL;

END

BEGIN

    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])

    VALUES (N'20210602040448_NewAutoExpand', N'3.1.4');

END;


GO