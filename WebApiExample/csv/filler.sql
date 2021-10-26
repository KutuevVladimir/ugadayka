start transaction;

insert into Tracks values(1,'Don\'t Speak','No Doubt','https://cdns-preview-c.dzcdn.net/stream/c-c2a94aa4393bc1c91788faf93e492c10-10.mp3');
insert into Tracks values(2,'(I Just) Died In Your Arms','Cutting Crew','https://cdns-preview-4.dzcdn.net/stream/c-49a09b3cf342cd21c048a48d4b9f0463-6.mp3');
insert into Tracks values(3,'Sugar','Maroon 5','https://cdns-preview-0.dzcdn.net/stream/c-05528035755eec7d1fb4807c78972b29-3.mp3');
insert into Tracks values(4,'Лисий-кисий','Neverlove','https://www.deezer.com/ru/track/1524493952');

insert into Players values(1,null,null,'TestPlayer',null,100,null,null);

insert into Playlists values(1,'TestPlayList',null,1);
insert into Playlists values(2,'TestPlayList2',null,1);

insert into Rooms values(1,'TestRoom',null,1,10,0,null,null,1);

insert into PlaylistsToTracks values(1,1);
insert into PlaylistsToTracks values(1,2);
insert into PlaylistsToTracks values(1,3);
insert into PlaylistsToTracks values(2,4);

commit;
