download docker desktop + WSL 2 (kernel linux)

para verificar se foi instalado correntamente
```bat
docker -v
```
criar container postgres1 com linux e o postgres no container
```bat
docker run -e POSTGRES_PASSWORD=Postgres2020! -p 5432:5432 --name postgres1 -h postgres1 -d postgres
```
acessar o bash
```bat
docker exec -it postgres1 bash
```
acessar o terminal iterativo psql pelo bash
```console
root@postgres1:/# psql -h localhost -p 5432 -U postgres -W
```
