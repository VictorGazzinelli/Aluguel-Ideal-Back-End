/* AluguelIdealDatabaseMigration migrating =================================== */

/* Beginning Transaction */
/* ExecuteSqlStatement DROP TABLE IF EXISTS advertisement */
DROP TABLE IF EXISTS advertisement
/* => 0,1885951s */

/* ExecuteSqlStatement DROP TABLE IF EXISTS contact */
DROP TABLE IF EXISTS contact
/* => 0,1564701s */

/* CreateTable advertisement */
CREATE TABLE "public"."advertisement" ("id" serial NOT NULL, "title" varchar(255) NOT NULL, "deleted_at" timestamp, CONSTRAINT "PK_advertisement" PRIMARY KEY ("id"));
/* => 0,202055s */

/* CreateTable contact */
CREATE TABLE "public"."contact" ("id" serial NOT NULL, "name" varchar(255) NOT NULL, "email" varchar(255) NOT NULL, "phone" varchar(255) NOT NULL, "deleted_at" timestamp, CONSTRAINT "PK_contact" PRIMARY KEY ("id"));
/* => 0,1648203s */

INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Sed omnis odio enim aut.',NULL);
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Rerum illum labore excepturi et dolorem asperiores ex minima modi.',NULL);
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Reiciendis esse aut ut voluptatem nihil quod est qui molestiae.',NULL);
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Labore explicabo odio et repudiandae quasi laborum.',NULL);
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Blanditiis et dolores sapiente itaque soluta ipsum.',NULL);
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Ducimus eos commodi molestiae est dicta odit.',NULL);
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Necessitatibus ut sit ipsa molestiae eveniet.',NULL);
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Natus repellat atque eos saepe.',NULL);
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Laudantium aut quae aut ab magni repellendus dolores aut minus.',NULL);
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Sapiente distinctio quia dolorum et odit aut sunt eaque.',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Lara Costa','Liz47@example.org','+558316115823',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Clara Martins','Julia.Pereira@example.net','+552245847244',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Célia Oliveira','Carla.Costa@example.org','+555907841288',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Marcela Reis','Alicia_Reis@example.com','+553239032179',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Emanuel Oliveira','Rafaela47@example.org','+555162847330',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Ana Laura Melo','Elisio5@example.net','+550463429042',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Gabriel Martins','Samuel.Pereira49@example.net','+555399122804',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Matheus Xavier','AnaLaura.Moreira99@example.org','+559321961938',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Benjamin Pereira','Marina.Barros@example.com','+552010061607','2020-12-11T19:14:58');
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Pietro Pereira','Lucca81@example.org','+559503521100',NULL);
/* -> 20 Insert operations completed in 00:00:03.1018322 taking an average of 00:00:00.1550916 */
/* Committing Transaction */
/* AluguelIdealDatabaseMigration migrated */
/* => 0,3192183s */

