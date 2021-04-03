/* AluguelIdealDatabaseMigration migrating =================================== */

/* Beginning Transaction */
/* ExecuteSqlStatement DROP TABLE IF EXISTS advertisement */
DROP TABLE IF EXISTS advertisement
/* => 0,1629921s */

/* ExecuteSqlStatement DROP TABLE IF EXISTS contact */
DROP TABLE IF EXISTS contact
/* => 0,1575153s */

/* CreateTable advertisement */
CREATE TABLE "public"."advertisement" ("id" serial NOT NULL, "title" varchar(255) NOT NULL, "deleted_at" timestamp, CONSTRAINT "PK_advertisement" PRIMARY KEY ("id"));
/* => 0,1726756s */

/* CreateTable contact */
CREATE TABLE "public"."contact" ("id" serial NOT NULL, "name" varchar(255) NOT NULL, "email" varchar(255) NOT NULL, "phone" varchar(255) NOT NULL, "deleted_at" timestamp, CONSTRAINT "PK_contact" PRIMARY KEY ("id"));
/* => 0,1789953s */

INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Quia consequatur nulla ratione officia.',NULL);
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Asperiores quis autem nulla quae.',NULL);
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Qui nisi quidem enim sint.',NULL);
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Est porro sed sequi dicta qui.',NULL);
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Voluptates voluptate esse sed.',NULL);
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Commodi ut earum earum quia molestiae rem omnis et error.',NULL);
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Pariatur fugiat iure expedita.',NULL);
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Distinctio commodi aliquam ipsum et consectetur cumque dolores atque.',NULL);
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Ut cum et laborum.',NULL);
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Sit dolore et quas.',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Helena Silva','Antonio.Silva89@example.org','+555206749730',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Ígor Batista','Vitor.Reis@example.com','+551360874114',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Isabela Carvalho','JoaoMiguel_Macedo7@example.org','+559249507695',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Júlia Moraes','Leonardo14@example.net','+559238326007',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Ana Clara Xavier','Fabricio_Moreira42@example.com','+558982502997',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Benjamin Saraiva','Pedro.Carvalho@example.org','+556996994350','2020-06-23T07:15:41');
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Maria Clara Santos','Felicia.Moreira@example.net','+552206130392',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Maria Alice Moraes','Arthur.Batista88@example.net','+552176967042',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Gustavo Moraes','Samuel_Costa@example.org','+553954033548',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Isabella Costa','Roberto.Santos@example.com','+556459978031',NULL);
/* -> 20 Insert operations completed in 00:00:03.1217236 taking an average of 00:00:00.1560861 */
/* Committing Transaction */
/* AluguelIdealDatabaseMigration migrated */
/* => 0,3274681s */

