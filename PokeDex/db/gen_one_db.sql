/* check whether database exists and drop it if it does */
IF EXISTS (SELECT 1 FROM master.dbo.sysdatabases WHERE NAME = 'gen_one_db')

BEGIN
	DROP DATABASE gen_one_db
	print '' print '*** dropping database gen_one_db ***'
END
GO

print '' print '*** creating database gen_one_db ***'
GO
CREATE DATABASE	gen_one_db
GO

print '' print '*** using database gen_one_db ***'
GO
USE [gen_one_db]
GO

print '' print '*** creating pokemon table ***'
GO
CREATE TABLE [dbo].[Pokemon](
	[pokedex_number]		[INT] 			NOT NULL,
    [pokemon_name]			[NVARCHAR](50)	NOT NULL,
    [type_one]				[NVARCHAR](10)	NOT NULL,
    [type_two]				[NVARCHAR](10)	NULL,
	[catch_rate]			[INT]			NOT NULL,
    [base_HP]				[INT]			NOT NULL,
    [base_attack]			[INT]			NOT NULL,
    [base_defense]			[INT]			NOT NULL,
    [base_special_attack]	[INT]			NOT NULL,
    [base_special_defense]	[INT]			NOT NULL,
    [base_speed] 			[INT]			NOT NULL,
    [pokemon_description] 	[TEXT]			NOT NULL,
	CONSTRAINT [pk_pokemon_name] PRIMARY KEY([pokemon_name]),
	CONSTRAINT [ak_pokedex_number] UNIQUE([pokedex_number] ASC)
    )
GO

print '' print '*** creating pokemon test data ***'
GO
INSERT INTO [dbo].[Pokemon]
		([pokedex_number], [pokemon_name], [type_one], [type_two], 
		[catch_rate], [base_HP], [base_attack], [base_defense], 
		[base_special_attack], [base_special_defense], [base_speed], [pokemon_description])
	VALUES
		(001,'Bulbasaur','GRASS','POISON',45,45,49,49,65,65,45,'A strange seed was planted on its back at birth. The plant sprouts and grows with this Pokemon.'),
		(002,'Ivysaur','GRASS','POISON',45,60,62,63,80,80,60,'When the bulb on its back grows large, it appears to lose the ability to stand on its hind legs.'),
		(003,'Venusaur','GRASS','POISON',45,80,82,83,100,100,80,'The plant blooms when it is absorbing solar energy. It stays on the move to seek sunlight.'),
		(019,'Rattata','NORMAL','None',255,30,56,35,25,35,72,'Bites anything when it attacks. Small and very quick, it is a common sight in many places.'),
		(020,'Raticate','NORMAL','None',90,55,81,60,50,70,97,'It uses its whiskers to maintain its balance. It apparently slows down if they are cut off.')
GO


print '' print '*** creating Location table ***'
GO
CREATE TABLE [dbo].[Location](
	[location_name]				[NVARCHAR](50)	NOT NULL
	,[description]				[TEXT]			NOT NULL
	,CONSTRAINT [pk_location_name] PRIMARY KEY ([location_name] ASC)
)
GO

print '' print '*** creating Location test data ***'
GO
INSERT INTO [dbo].[Location]
		([location_name], [description])
	VALUES
		('Pallet Town','randome stuff')
		,('Viridian City','randome stuff')
		,('Cerulean City','randome stuff')
		,('Vermilion City','randome stuff')
		,('Celadon City','randome stuff')
		,('Fuchsia City','randome stuff')
		,('Cinnabar Island','randome stuff')
		,('Indigo Plateau','randome stuff')
		,('Route 1','randome stuff')
		,('Route 2','randome stuff')
		,('Route 3','randome stuff')
		,('Route 4','randome stuff')
		,('Route 5','randome stuff')
		,('Route 6','randome stuff')
		,('Route 7','randome stuff')
		,('Route 8','randome stuff')
		,('Route 9','randome stuff')
		,('Route 10','randome stuff')
		,('Route 11','randome stuff')
		,('Route 12','randome stuff')
		,('Route 13','randome stuff')
		,('Route 14','randome stuff')
		,('Route 15','randome stuff')
		,('Route 16','randome stuff')
		,('Route 17','randome stuff')
		,('Route 18','randome stuff')
		,('Route 19','randome stuff')
		,('Route 20','randome stuff')
		,('Route 21','randome stuff')
		,('Route 22','randome stuff')
		,('Route 23','randome stuff')
		,('Route 24','randome stuff')
		,('Route 25','randome stuff')
		,('Professor Oaks Laboratory','randome stuff')
		,('Digletts Cave','randome stuff')
		,('Viridian Forest','randome stuff')
		,('Mt. Moon 1F','randome stuff')
		,('Mt. Moon B1F','randome stuff')
		,('Mt. Moon B2F','randome stuff')
		,('Underground Path (Routes 5-6)','randome stuff')
		,('Rock Tunnel 1F','randome stuff')
		,('Power Plant','randome stuff')
		,('Victory Road 1F','randome stuff')
		,('Victory Road 2F','randome stuff')
		,('Victory Road 3F','randome stuff')
		,('Celadon Condominiums','randome stuff')
		,('Pokemon Tower','randome stuff')
		,('Safari Zone Center Area (hub)','randome stuff')
		,('Safari Zone Center Area (east)','randome stuff')
		,('Safari Zone Area 2','randome stuff')
		,('Safari Zone Area 3','randome stuff')
		,('Seafoam Islands 1F','randome stuff')
		,('Seafoam Islands B1F','randome stuff')
		,('Seafoam Islands B2F','randome stuff')
		,('Seafoam Islands B3F','randome stuff')
		,('Seafoam Islands B4F','randome stuff')
		,('Cinnabar Lab','randome stuff')
		,('Pokemon Mansion on Cinnabar Island','randome stuff')
		,('Fighting Dojo in Saffron City','randome stuff')
		,('Silph Co. in Saffron City','randome stuff')
		,('Cerulean Cave 1F','randome stuff')
		,('Cerulean Cave 2F','randome stuff')
		,('Cerulean Cave B1F','randome stuff')
		,('Pewter City','randome stuff')
		,('S.S. Anne', 'randome stuff')
		,('Team Rocket Hideout', 'randome stuff')
		,('Celadon Department Store', 'random stuff')

print '' print '*** creating PokemonLocation table ***'
GO
CREATE TABLE [dbo].[PokemonLocation](
	[location_name]				[NVARCHAR](50)		NOT NULL
	,[pokemon_name]				[NVARCHAR](50)		NOT NULL
	,[game_name]				[NVARCHAR](6)		NOT NULL
    ,[how_found]				[TEXT]				NOT NULL
    ,[level_found]				[NVARCHAR](300)		NOT NULL
    ,[species_encounter_rate]	[TEXT]				NOT NULL
	, CONSTRAINT [pk_location_name_pokemon_name_level_found_game_name] 
		PRIMARY KEY ([location_name], [pokemon_name], [level_found], [game_name] ASC)
	, CONSTRAINT [fk_pokemon_name_Pokemonlocation] 
		FOREIGN KEY([pokemon_name]) REFERENCES [dbo].[Pokemon]([pokemon_name])
	, CONSTRAINT [fk_location_name_Pokemolocation]
		FOREIGN KEY([location_name]) REFERENCES [dbo].[Location]([location_name])
)
GO

print '' print '*** creating PokemonLocation test data ***'
GO
INSERT INTO [dbo].[PokemonLocation]
		([location_name], [pokemon_name], [game_name], [how_found], 
		[level_found], [species_encounter_rate])
	VALUES
		('Route 1','Rattata','RB','Grass','2','50%')
		,('Route 1','Rattata','RB','Grass','3','50%')
		,('Route 1','Rattata','RB','Grass','4','50%')
		,('Route 1','Rattata','Y','Grass','2','30%')
		,('Route 1','Rattata','Y','Grass','3','30%')
		,('Route 1','Rattata','Y','Grass','4','30%')
		,('Route 9','Raticate','Y','Grass','20','4%')
		,('Professor Oaks Laboratory','Bulbasaur','RB','Starter Pokemon','5','Only One')
		,('Cerulean City','Bulbasaur','Y','Gift from a girl if Pikachus friendship is high enough, In the house left of the Pokemon Center','10','Only One')
GO

print '' print '*** creating Evolution table ***'
GO
CREATE TABLE [dbo].[Evolution](
    [reactant]				[NVARCHAR](50)	NOT NULL
	,[evolution_condition]	[TEXT]			NOT NULL
	,[evolves_into] 		[NVARCHAR](50)	NOT NULL
	, CONSTRAINT [pk_reactant_evolves_into] 
		PRIMARY KEY ([reactant] ASC, [evolves_into] ASC)
    , CONSTRAINT [fk_evolves_into_Evolvolution]
		FOREIGN KEY([evolves_into]) REFERENCES [dbo].[Pokemon]([pokemon_name])
    , CONSTRAINT [fk_reactant_Evolvolution]
		FOREIGN KEY([reactant]) REFERENCES [dbo].[Pokemon]([pokemon_name])
)
GO

print '' print '*** creating Evolution test data ***'
GO
INSERT INTO [dbo].[Evolution]
		([reactant], [evolution_condition], [evolves_into])
	VALUES
		('Bulbasaur','LEVEL 16','Ivysaur')
		,('Ivysaur','LEVEL 32','Venusaur')
		,('Rattata','LEVEL 20','Raticate')
GO

print '' print '*** creating Role table ***'
GO
CREATE TABLE [dbo].[Role](
	[role_name]		[NVARCHAR](10)	NOT NULL			
	,[role_description]	[NVARCHAR](250) NULL
	,CONSTRAINT [role_name] PRIMARY KEY ([role_name])
)
GO

print '' print '*** creating Role test data ***'
GO
INSERT INTO [dbo].[Role]
		([role_name], [role_description])
	VALUES
		('admin', 'is alowed to use all crud functions'),
		('researcher', 'is alowed to add and search pokemon related tabels'),
		('user', 'is alowed to search through pokemon realtaed tables')

print '' print '*** creating Member table ***'
GO
CREATE TABLE [dbo].[Member](
	[member_id]			[int]			IDENTITY(1000000, 1)	NOT NULL
	,[email] 			[NVARCHAR](100)							NOT NULL
	,[first_name]		[NVARCHAR](50)							NOT NULL
	,[last_name]		[NVARCHAR](100)							NOT NULL
	,[password_hash]	[NVARCHAR](100)							NOT NULL DEFAULT 
		'9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E'
	,[active]			[BIT]			NOT NULL DEFAULT 1
	,[role]				[NVARCHAR](10)	NOT NULL DEFAULT 'user'
	,CONSTRAINT [pk_member_id] PRIMARY KEY([member_id] ASC)
	,CONSTRAINT [ak_email] UNIQUE([email] ASC)
	,CONSTRAINT [fk_role_name]
		FOREIGN KEY([role]) REFERENCES [dbo].[Role]([role_name])
)
GO

print '' print '*** creating Member test data ***'
GO
INSERT INTO [dbo].[Member]
		([email], [first_name], [last_name], [role])
	VALUES
		('hu@company.com', 'Hu', 'Man', 'admin')
		,('guss@company.com', 'Guss', 'Faick', 'user')
		,('rich@company.com', 'Richy', 'Fakeman', 'researcher')
GO


print '' print '*** USER PROCEDURE FOR USERS ***'
GO

print '' print '*** creating sp_authenticate_user ***'
GO
CREATE PROCEDURE [dbo].[sp_authenticate_user]
	(
	@email			[nvarchar](100),
	@password_hash	[nvarchar](100)
	)
AS
	BEGIN
		SELECT COUNT(email)
		FROM Member
		WHERE email = @email
		AND password_hash = @password_hash
		AND active = 1
	END
GO

print '' print '*** creating sp_update_passwordhash ***'
GO
CREATE PROCEDURE [dbo].[sp_update_passwordhash]
	(
	@email				[nvarchar](100),
	@Oldpassword_hash	[nvarchar](100),
	@Newpassword_hash	[nvarchar](100)
	)
AS
	BEGIN
		UPDATE Member
			SET password_hash = @Newpassword_hash
			WHERE email = @email
			AND password_hash = @Oldpassword_hash
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** creating sp_select_user_by_email ***'
GO
CREATE PROCEDURE [dbo].[sp_select_user_by_email]
	(
	@email				[nvarchar](100)
	)
AS
	BEGIN
		SELECT member_id, email, first_name, last_name, active, role
		FROM Member
		WHERE email = @email
	END
GO

print '' print '*** creating sp_select_roles_by_member_id ***'
GO
CREATE PROCEDURE [dbo].[sp_select_roles_by_member_id]
	(
	@member_id				[int]
	)
AS
	BEGIN
		SELECT role
		FROM Member
		WHERE member_id = @member_id
	END
GO

print '' print '*** creating sp_update_member_profile_data ***'
GO
CREATE PROCEDURE [dbo].[sp_update_member_profile_data]
	(
	@member_id				[int],
	@Newemail				[nvarchar](100),
	@Newfirst_name			[nvarchar](50),
	@Newlast_name			[nvarchar](50),
	
	@Oldlast_name			[nvarchar](50),
	@Oldfirst_name			[nvarchar](50),
	@Oldemail				[nvarchar](100)
	)
AS
	BEGIN
		UPDATE Member
			SET email = @Newemail,
				first_name = @Newfirst_name,
				last_name = @Newlast_name
			WHERE member_id = @member_id
			And email = @Oldemail
			AND first_name = @Oldfirst_name
			AND	last_name = @Oldlast_name
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** creating sp_insert_new_user ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_new_user]
(
	@email				[nvarchar](100),
	@first_name			[nvarchar](50),
	@last_name			[nvarchar](50)
)
AS
	BEGIN
		INSERT INTO [dbo].[Member]
			([email], [first_name], [last_name])
		VALUES
			(@email, @first_name, @last_name)
		SELECT SCOPE_IDENTITY()
	END
GO

print '' print '*** creating sp_select_all_members ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_members]
AS
	BEGIN
		SELECT member_id, email, first_name, last_name, role, active
		FROM Member
		ORDER BY last_name ASC
	END
GO

print '' print '*** creating sp_select_members_by_active ***'
GO
CREATE PROCEDURE [dbo].[sp_select_members_by_active]
(
	@active			[bit]
)
AS
	BEGIN
		SELECT member_id, email, first_name, last_name, role, active
		FROM Member
		WHERE active = @active
		ORDER BY last_name ASC
	END
GO

print '' print '*** creating sp_select_member_by_id ***'
GO
CREATE PROCEDURE [dbo].[sp_select_member_by_id]
	(
	@member_id				[int]
	)
AS
	BEGIN
		SELECT member_id, email, first_name, last_name, role, active
		FROM Member
		WHERE member_id = @member_id
	END
GO

print '' print '*** creating sp_reset_passwordhash ***'
GO
CREATE PROCEDURE [dbo].[sp_reset_passwordhash]
	(
	@email				[nvarchar](100)
	)
AS
	BEGIN
		UPDATE Member
			SET password_hash = 
			'9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E'
			WHERE email = @email
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** creating sp_safely_deactivate_member ***'
GO
CREATE PROCEDURE [dbo].[sp_safely_deactivate_member]
	(
	@member_id				[int]
	)
AS
	BEGIN
		DECLARE @Admins int;

		SELECT @Admins = COUNT(role)
		FROM Member
		WHERE role = 'admin'
			AND member_id = @member_id;

		IF @Admins = 1
			BEGIN
				RETURN 0
			END
		ELSE
			BEGIN
				UPDATE Member
					SET Active = 0
					WHERE member_id = @member_id
				RETURN @@ROWCOUNT	
			END
	END
GO

print '' print '*** creating sp_reactivate_member ***'
GO
CREATE PROCEDURE [dbo].[sp_reactivate_member]
	(
	@member_id				[int]
	)
AS
	BEGIN
		UPDATE Member
			SET active = 1
			WHERE member_id = @member_id
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** creating sp_insert_new_pokemon ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_new_pokemon]
	(
		@pokedex_number			[INT],
		@pokemon_name			[NVARCHAR](50),
		@type_one				[NVARCHAR](10),
		@type_two				[NVARCHAR](10),
		@catch_rate				[INT],
		@base_HP				[INT],
		@base_attack			[INT],
		@base_defense			[INT],
		@base_special_attack	[INT],
		@base_special_defense	[INT],
		@base_speed 			[INT],
		@pokemon_description 	[TEXT]
	)
AS
	BEGIN
		INSERT INTO [dbo].[Pokemon]
			([pokedex_number], [pokemon_name], [type_one], [type_two], 
			[catch_rate], [base_HP], [base_attack], [base_defense], 
			[base_special_attack], [base_special_defense], [base_speed], 
			[pokemon_description])
		VALUES
			(@pokedex_number, @pokemon_name, @type_one, @type_two, 
			@catch_rate, @base_HP, @base_attack, @base_defense, 
			@base_special_attack, @base_special_defense, 
			@base_speed, @pokemon_description)
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** creating sp_select_all_pokemon ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_pokemon]
AS
	BEGIN
		SELECT Pokemon.pokedex_number, Pokemon.pokemon_name, Pokemon.type_one, Pokemon.type_two, Pokemon.catch_rate, Pokemon.base_HP, 
		Pokemon.base_attack, Pokemon.base_defense, Pokemon.base_special_attack, Pokemon.base_special_defense, Pokemon.base_speed, Pokemon.pokemon_description
		FROM Pokemon
		ORDER BY pokedex_number ASC
	END
GO

print '' print '*** creating sp_select_pokemon_by_name ***'
GO
CREATE PROCEDURE [dbo].[sp_select_pokemon_by_name]
(
	@pokemon_name	[NVARCHAR](50)
)
AS
	BEGIN
		SELECT pokedex_number, pokemon_name, type_one, type_two, catch_rate, base_HP, 
		base_attack, base_defense, base_special_attack, base_special_defense, base_speed, 
		pokemon_description
		FROM Pokemon
		WHERE pokemon_name = @pokemon_name
	END
GO

print '' print '*** creating sp_select_pokemon_by_pokedex_number ***'
GO
CREATE PROCEDURE [dbo].[sp_select_pokemon_by_pokedex_number]
(
	@pokedex_number	[INT]
)
AS
	BEGIN
		SELECT pokedex_number, pokemon_name, type_one, type_two, catch_rate, base_HP, 
		base_attack, base_defense, base_special_attack, base_special_defense, base_speed, 
		pokemon_description
		FROM Pokemon
		WHERE pokedex_number = @pokedex_number
	END
GO

print '' print '*** creating sp_select_pokemon_by_type ***'
GO
CREATE PROCEDURE [dbo].[sp_select_pokemon_by_type]
(
	@pokemon_type	[NVARCHAR](10)
)
AS
	BEGIN
		SELECT pokedex_number, pokemon_name, type_one, type_two, catch_rate, base_HP, 
		base_attack, base_defense, base_special_attack, base_special_defense, base_speed, 
		pokemon_description
		FROM Pokemon
		WHERE type_one = @pokemon_type 
			OR type_two = @pokemon_type
		ORDER BY pokedex_number ASC
	END
GO

print '' print '*** creating sp_update_pokemon ***'
GO
CREATE PROCEDURE [dbo].[sp_update_pokemon]
(
	@pokedex_number			[INT],
	@pokemon_name			[NVARCHAR](50),
	@Old_type_one				[NVARCHAR](10),
	@Old_type_two				[NVARCHAR](10),
	@Old_catch_rate				[INT],
	@Old_base_HP				[INT],
	@Old_base_attack			[INT],
	@Old_base_defense			[INT],
	@Old_base_special_attack	[INT],
	@Old_base_special_defense	[INT],
	@Old_base_speed	 			[INT],
	@Old_pokemon_description 	[TEXT],
	@New_type_one				[NVARCHAR](10),
	@New_type_two				[NVARCHAR](10),
	@New_catch_rate				[INT],
	@New_base_HP				[INT],
	@New_base_attack			[INT],
	@New_base_defense			[INT],
	@New_base_special_attack	[INT],
	@New_base_special_defense	[INT],
	@New_base_speed	 			[INT],
	@New_pokemon_description 	[TEXT]
)
AS
	BEGIN
		UPDATE Pokemon
			SET type_one = @New_type_one,
					type_two = @New_type_two,
					catch_rate = @New_catch_rate,
					base_HP = @New_base_HP,
					base_attack = @New_base_attack,
					base_defense = @New_base_defense,
					base_special_attack = @New_base_special_attack,
					base_special_defense = @New_base_special_defense,
					base_speed = @New_base_speed,
					pokemon_description = @New_pokemon_description
			WHERE pokedex_number = @pokedex_number
					AND pokemon_name = @pokemon_name
					AND type_one = @Old_type_one
					AND type_two = @Old_type_two
					AND catch_rate = @Old_catch_rate
					AND base_HP = @Old_base_HP
					AND base_attack = @Old_base_attack
					AND base_defense = @Old_base_defense
					AND base_special_attack = @Old_base_special_attack
					AND base_special_defense = @Old_base_special_defense
					AND base_speed = @Old_base_speed
					-- AND pokemon_description LIKE @Old_pokemon_description
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** creating sp_delete_pokemon_by_name ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_pokemon_by_name]
	(
		@pokemon_name	[NVARCHAR](50)
	)
AS
	BEGIN
		DELETE Pokemon 
			WHERE pokemon_name = @pokemon_name
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** creating sp_insert_new_location ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_new_location]
	(
	@location_name		[nvarchar](50),
	@description		[TEXT]
	)
AS
	BEGIN
		INSERT INTO [dbo].[Location]
			([location_name], [description])
		VALUES
			(@location_name, @description)
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** creating sp_select_all_location ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_location]
AS
	BEGIN
		SELECT location_name, description
		FROM Location
		ORDER BY location_name ASC
	END
GO

print '' print '*** creating sp_select_location_by_name ***'
GO
CREATE PROCEDURE [dbo].[sp_select_location_by_name]
	(
		@location_name	[NVARCHAR](50)
	)
AS
	BEGIN
		SELECT location_name, description
		FROM Location
		WHERE location_name = @location_name
	END
GO

print '' print '*** creating sp_update_location_by_name ***' -- fix
GO
CREATE PROCEDURE [dbo].[sp_update_location_by_name]
	(
		@location_name			[nvarchar](50),
		@Old_description		[TEXT],
		@New_description		[TEXT]
	)
AS
	BEGIN
		UPDATE Location
			SET description = @New_description
			WHERE location_name = @location_name
					AND description LIKE @Old_description
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** creating sp_delete_location_by_name ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_location_by_name]
	(
		@location_name	[NVARCHAR](50)
	)
AS
	BEGIN
		DELETE Location
			WHERE location_name = @location_name
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** creating sp_insert_new_pokemon_location ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_new_pokemon_location]
	(
	@location_name				[nvarchar](50),
	@pokemon_name				[NVARCHAR](50),
	@game_name					[NVARCHAR](6),
	@how_found					[TEXT],
	@level_found				[NVARCHAR](300),
	@species_encounter_rate		[TEXT]
	)
AS
	BEGIN
		INSERT INTO [dbo].[PokemonLocation]
		([location_name], [pokemon_name], [game_name], [how_found], 
		[level_found], [species_encounter_rate])
		VALUES
			(@location_name, @pokemon_name, @game_name, @how_found, 
			@level_found, @species_encounter_rate)
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** creating sp_select_all_pokemon_location ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_pokemon_location]
AS
	BEGIN
		SELECT location_name, pokemon_name, game_name, how_found, 
		level_found, species_encounter_rate
		FROM PokemonLocation
		ORDER BY location_name ASC, pokemon_name ASC, game_name ASC
	END
GO

print '' print '*** creating sp_select_pokemon_location_by_location ***'
GO
CREATE PROCEDURE [dbo].[sp_select_pokemon_location_by_location]
	(
		@location_name				[nvarchar](50)
	)
AS
	BEGIN
		SELECT location_name, pokemon_name, game_name, how_found, 
		level_found, species_encounter_rate
		FROM PokemonLocation
		WHERE location_name = @location_name
		ORDER BY location_name ASC, pokemon_name ASC, game_name ASC
	END
GO

print '' print '*** creating sp_select_pokemon_location_by_pokemon_name ***'
GO
CREATE PROCEDURE [dbo].[sp_select_pokemon_location_by_pokemon_name]
	(
		@pokemon_name				[NVARCHAR](50)
	)
AS
	BEGIN
		SELECT location_name, pokemon_name, game_name, how_found, 
		level_found, species_encounter_rate
		FROM PokemonLocation
		WHERE pokemon_name = @pokemon_name
		ORDER BY location_name ASC, pokemon_name ASC, game_name ASC
	END
GO

print '' print '*** creating sp_select_pokemon_location_by_game_name ***'
GO
CREATE PROCEDURE [dbo].[sp_select_pokemon_location_by_game_name]
	(
		@game_name					[NVARCHAR](6)
	)
AS
	BEGIN
		SELECT location_name, pokemon_name, game_name, how_found, 
		level_found, species_encounter_rate
		FROM PokemonLocation
		WHERE game_name = @game_name
		ORDER BY location_name ASC, pokemon_name ASC, game_name ASC
	END
GO

print '' print '*** creating sp_update_pokemon_location ***'
GO
CREATE PROCEDURE [dbo].[sp_update_pokemon_location]
(
	@Old_location_name				[nvarchar](50),
	@Old_pokemon_name				[NVARCHAR](50),
	@Old_game_name					[NVARCHAR](6),
	@Old_how_found					[TEXT],
	@Old_level_found				[NVARCHAR](300),
	@Old_species_encounter_rate		[TEXT],
	@New_location_name				[nvarchar](50),
	@New_pokemon_name				[NVARCHAR](50),
	@New_game_name					[NVARCHAR](6),
	@New_how_found					[TEXT],
	@New_level_found				[NVARCHAR](300),
	@New_species_encounter_rate		[TEXT]
)
AS
	BEGIN
		UPDATE PokemonLocation
			SET location_name = @New_location_name,
					pokemon_name = @New_pokemon_name,
					game_name = @New_game_name,
					how_found = @New_how_found,
					level_found = @New_level_found,
					species_encounter_rate = @New_species_encounter_rate
			WHERE location_name = @Old_location_name
					AND pokemon_name = @Old_pokemon_name
					AND game_name = @Old_game_name
					AND how_found LIKE @Old_how_found
					AND level_found = @Old_level_found
					AND species_encounter_rate LIKE @Old_species_encounter_rate
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** creating sp_delete_one_pokemon_location ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_one_pokemon_location]
	(
		@location_name		[NVARCHAR](50),
		@pokemon_name		[NVARCHAR](50),
		@level_found		[NVARCHAR](300),
		@game_name			[NVARCHAR](6)
	)
AS
	BEGIN
		DELETE
		FROM PokemonLocation
			WHERE location_name = @location_name
			AND pokemon_name = @pokemon_name
			AND level_found = @level_found
			AND game_name = @game_name
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** creating sp_insert_new_evolution ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_new_evolution]
	(
	@reactant				[NVARCHAR](50),
	@evolution_condition	[TEXT],
	@evolves_into 		[NVARCHAR](50)
	)
AS
	BEGIN
		INSERT INTO [dbo].[Evolution]
		([reactant], [evolution_condition], [evolves_into])
		VALUES
			(@reactant, @evolution_condition, @evolves_into)
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** creating sp_select_all_evolution ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_evolution]
AS
	BEGIN
		SELECT reactant, evolution_condition, evolves_into
		FROM Evolution
		ORDER BY reactant ASC
	END
GO

print '' print '*** creating sp_select_evolution_by_reactant ***'
GO
CREATE PROCEDURE [dbo].[sp_select_evolution_by_reactant]
	(
		@reactant	[NVARCHAR](50)
	)
AS
	BEGIN
		SELECT reactant, evolution_condition, evolves_into
		FROM Evolution
		WHERE reactant = @reactant
		ORDER BY reactant ASC
	END
GO

print '' print '*** creating sp_select_evolution_by_evolves_into ***'
GO
CREATE PROCEDURE [dbo].[sp_select_evolution_by_evolves_into]
	(
		@evolves_into	[NVARCHAR](50)
	)
AS
	BEGIN
		SELECT reactant, evolution_condition, evolves_into
		FROM Evolution
		WHERE evolves_into = @evolves_into
		ORDER BY reactant ASC
	END
GO

print '' print '*** creating sp_update_evolution ***'
GO
CREATE PROCEDURE [dbo].[sp_update_evolution]
(
	@Old_reactant				[NVARCHAR](50),
	@Old_evolution_condition	[TEXT],
	@Old_evolves_into 			[NVARCHAR](50),
	@New_reactant				[NVARCHAR](50),
	@New_evolution_condition	[TEXT],
	@New_evolves_into 			[NVARCHAR](50)
)
AS
	BEGIN
		UPDATE Evolution
			SET reactant = @New_reactant,
					evolution_condition = @New_evolution_condition,
					evolves_into = @New_evolves_into
			WHERE reactant = @Old_reactant
					AND evolution_condition LIKE @Old_evolution_condition
					AND evolves_into = @Old_evolves_into
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** creating sp_delete_one_evolution ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_one_evolution]
	(
		@reactant				[NVARCHAR](50),
		@evolution_condition	[TEXT],
		@evolves_into 			[NVARCHAR](50)
	)
AS
	BEGIN
		DELETE
		FROM Evolution
			WHERE reactant = @reactant
				AND evolution_condition LIKE @evolution_condition
				AND evolves_into = @evolves_into
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** creating sp_safely_change_member_role ***'
GO
CREATE PROCEDURE [dbo].[sp_safely_change_member_role]
	(
		@member_id		[int],
		@Oldrole		[NVARCHAR](10),
		@Newrole 		[NVARCHAR](10)
	)
AS
	BEGIN
		DECLARE @Admins int;

		SELECT @Admins = COUNT(role)
		FROM Member
		WHERE role = 'admin';

		IF @Oldrole = 'admin' AND @Admins = 1
			BEGIN
				RETURN 0
			END
		ELSE
			BEGIN
				UPDATE Member
					SET role = @Newrole
					WHERE member_id = @member_id
					AND role = @Oldrole
				RETURN @@ROWCOUNT	
			END
	END
GO

print '' print '*** creating sp_select_all_roles ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_roles]
AS
	BEGIN
		SELECT role_name
		FROM Role
		ORDER By role_name ASC
	END
GO