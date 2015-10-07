BEGIN TRAN

select * from seasons
select * from leagues

select top 10 * from fixtures order by id desc

-- league id 20 is this season
-- team leagues for season
select id from teamleagues where league_id = 20
-- fixtures for season
select * from fixtures 
where hometeamleague_id in (select id from teamleagues where league_id = 20)
or awayteamleague_id in (select id from teamleagues where league_id = 20)

-- Score different
select (hometeamscore - awayteamscore), * from fixtures 
where hometeamleague_id in (select id from teamleagues where league_id = 20)
or awayteamleague_id in (select id from teamleagues where league_id = 20)
order by (hometeamscore - awayteamscore)

-- Team with highest number of players averaging double figures 
select teamleague_id, count(teamleague_id) idcount
from
(
	select distinct pss.player_id, pss.pointspergame, pf.teamleague_id
	from playerseasonstats pss
	join playerfixtures pf on pss.player_id = pf.player_id
	where pf.teamleague_id in (select id from teamleagues where league_id = 20)
	and  pss.season_id = 9 and pss.pointspergame >= 10
	--order by pss.pointspergame desc
) as stuff
group by teamleague_id
order by idcount desc

select * from teamleagues where id in (76, 82)

select * from teamleagues where league_id = 20

select sum(totalfouls) allfouls, teamleague_id
from
	(
	-- Fouls for each player in each game
	select distinct pss.player_id, pss.totalfouls, pf.teamleague_id
	from playerseasonstats pss
	join playerfixtures pf on pss.player_id = pf.player_id
	where pf.teamleague_id in (select id from teamleagues where league_id = 20)
	and  pss.season_id = 9 
) as stuff
group by teamleague_id
order by allfouls desc
	
	select * from playerseasonstats where season_id = 9

select * from 



ROLLBACK