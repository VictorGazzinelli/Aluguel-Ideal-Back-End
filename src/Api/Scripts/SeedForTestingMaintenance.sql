-- UP
INSERT INTO "public"."city" ("id","name") VALUES ('29068e9e-39ba-4bb4-b743-a7ec1c4c5be0','Belo Horizonte');INSERT INTO "public"."city" ("id","name") VALUES ('aa97dd1d-3536-467e-8879-088fa06f020a','São Paulo');INSERT INTO "public"."city" ("id","name") VALUES ('0e6fe16b-814e-4390-9ff2-3d3b304693b4','Rio de Janeiro');
-- DOWN
DELETE FROM "public"."city" WHERE "id" = '29068e9e-39ba-4bb4-b743-a7ec1c4c5be0' AND "name" = 'Belo Horizonte';DELETE FROM "public"."city" WHERE "id" = 'aa97dd1d-3536-467e-8879-088fa06f020a' AND "name" = 'São Paulo';DELETE FROM "public"."city" WHERE "id" = '0e6fe16b-814e-4390-9ff2-3d3b304693b4' AND "name" = 'Rio de Janeiro';
