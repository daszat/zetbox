-- cleanup
delete from Tasks
delete from Projekte_haben_Mitarbeiter
delete from Projekte
delete from Mitarbeiter

-- declare vars
declare @now datetime
declare @id_david int
declare @id_arthur int
declare @ma_david int
declare @ma_arthur int
declare @id_prj int

-- get identites
select @now = GETDATE();
select @id_david = ID from Identities where UserName = 'DASZ\David'
select @id_arthur = ID from Identities where UserName = 'DASZ\Arthur'

-- create Mitarbeiter
insert into Mitarbeiter(Name, fk_Identity, fk_CreatedBy, fk_ChangedBy, CreatedOn, ChangedOn) values('David', @id_david, @id_arthur, @id_arthur, @now, @now)
select @ma_david = SCOPE_IDENTITY()
insert into Mitarbeiter(Name, fk_Identity, fk_CreatedBy, fk_ChangedBy, CreatedOn, ChangedOn) values('Arthur', @id_arthur, @id_arthur, @id_arthur, @now, @now)
select @ma_arthur = SCOPE_IDENTITY()

-- create Projekte
insert into Projekte (Kundenname, Name, fk_CreatedBy, fk_ChangedBy, CreatedOn, ChangedOn) values('dasz.at', 'Gemeinsames Projekt', @id_arthur, @id_arthur, @now, @now)
select @id_prj = SCOPE_IDENTITY()
insert into Projekte_haben_Mitarbeiter (fk_Projekte, fk_Projekte_pos, fk_Mitarbeiter, fk_Mitarbeiter_pos) values(@id_prj, 0, @ma_arthur, 0)
insert into Projekte_haben_Mitarbeiter (fk_Projekte, fk_Projekte_pos, fk_Mitarbeiter, fk_Mitarbeiter_pos) values(@id_prj, 1, @ma_david, 1)
insert into Tasks (Name, Aufwand, fk_Projekt, fk_CreatedBy, fk_ChangedBy, CreatedOn, ChangedOn) values('Task Gemeinsames Projekt 1', 10, @id_prj, @id_arthur, @id_arthur, @now, @now)
insert into Tasks (Name, Aufwand, fk_Projekt, fk_CreatedBy, fk_ChangedBy, CreatedOn, ChangedOn) values('Task Gemeinsames Projekt 2', 20, @id_prj, @id_arthur, @id_arthur, @now, @now)
insert into Tasks (Name, Aufwand, fk_Projekt, fk_CreatedBy, fk_ChangedBy, CreatedOn, ChangedOn) values('Task Gemeinsames Projekt 3', 30, @id_prj, @id_david, @id_david, @now, @now)
insert into Tasks (Name, Aufwand, fk_Projekt, fk_CreatedBy, fk_ChangedBy, CreatedOn, ChangedOn) values('Task Gemeinsames Projekt 4', 40, @id_prj, @id_david, @id_david, @now, @now)

insert into Projekte (Kundenname, Name, fk_CreatedBy, fk_ChangedBy, CreatedOn, ChangedOn) values('David', 'Davids Projekt', @id_david, @id_david, @now, @now)
select @id_prj = SCOPE_IDENTITY()
insert into Projekte_haben_Mitarbeiter (fk_Projekte, fk_Projekte_pos, fk_Mitarbeiter, fk_Mitarbeiter_pos) values(@id_prj, 0, @ma_david, 0)
insert into Tasks (Name, Aufwand, fk_Projekt, fk_CreatedBy, fk_ChangedBy, CreatedOn, ChangedOn) values('Task Davids Projekt 1', 10, @id_prj, @id_david, @id_david, @now, @now)
insert into Tasks (Name, Aufwand, fk_Projekt, fk_CreatedBy, fk_ChangedBy, CreatedOn, ChangedOn) values('Task Davids Projekt 2', 20, @id_prj, @id_david, @id_david, @now, @now)
insert into Tasks (Name, Aufwand, fk_Projekt, fk_CreatedBy, fk_ChangedBy, CreatedOn, ChangedOn) values('Task Davids Projekt 3', 30, @id_prj, @id_david, @id_david, @now, @now)
insert into Tasks (Name, Aufwand, fk_Projekt, fk_CreatedBy, fk_ChangedBy, CreatedOn, ChangedOn) values('Task Davids Projekt 4', 40, @id_prj, @id_david, @id_david, @now, @now)

insert into Projekte (Kundenname, Name, fk_CreatedBy, fk_ChangedBy, CreatedOn, ChangedOn) values('Arthur', 'Arthurs Projekt', @id_arthur, @id_arthur, @now, @now)
select @id_prj = SCOPE_IDENTITY()
insert into Projekte_haben_Mitarbeiter (fk_Projekte, fk_Projekte_pos, fk_Mitarbeiter, fk_Mitarbeiter_pos) values(@id_prj, 0, @ma_arthur, 0)
insert into Tasks (Name, Aufwand, fk_Projekt, fk_CreatedBy, fk_ChangedBy, CreatedOn, ChangedOn) values('Task Arthurs Projekt 1', 10, @id_prj, @id_arthur, @id_arthur, @now, @now)
insert into Tasks (Name, Aufwand, fk_Projekt, fk_CreatedBy, fk_ChangedBy, CreatedOn, ChangedOn) values('Task Arthurs Projekt 2', 20, @id_prj, @id_arthur, @id_arthur, @now, @now)
insert into Tasks (Name, Aufwand, fk_Projekt, fk_CreatedBy, fk_ChangedBy, CreatedOn, ChangedOn) values('Task Arthurs Projekt 3', 30, @id_prj, @id_arthur, @id_arthur, @now, @now)
insert into Tasks (Name, Aufwand, fk_Projekt, fk_CreatedBy, fk_ChangedBy, CreatedOn, ChangedOn) values('Task Arthurs Projekt 4', 40, @id_prj, @id_arthur, @id_arthur, @now, @now)

exec RefreshRightsOn_Projekte
exec RefreshRightsOn_Tasks
exec RefreshRightsOn_Auftraege