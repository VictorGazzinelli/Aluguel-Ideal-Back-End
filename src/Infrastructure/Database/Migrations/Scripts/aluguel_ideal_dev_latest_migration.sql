/* AluguelIdealDatabaseMigration migrating =================================== */

/* Beginning Transaction */
/* ExecuteSqlStatement DROP TABLE IF EXISTS advertisement */
DROP TABLE IF EXISTS advertisement
/* => 0,1634174s */

/* ExecuteSqlStatement DROP TABLE IF EXISTS contact */
DROP TABLE IF EXISTS contact
/* => 0,1593085s */

/* CreateTable advertisement */
CREATE TABLE "public"."advertisement" ("id" serial NOT NULL, "title" varchar(255) NOT NULL, "deleted_at" timestamp, CONSTRAINT "PK_advertisement" PRIMARY KEY ("id"));
/* => 0,1907644s */

/* CreateTable contact */
CREATE TABLE "public"."contact" ("id" serial NOT NULL, "name" varchar(255) NOT NULL, "email" varchar(255) NOT NULL, "phone" varchar(255) NOT NULL, "deleted_at" timestamp, CONSTRAINT "PK_contact" PRIMARY KEY ("id"));
/* => 0,1713769s */

INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Deserunt et ut labore itaque maiores.',NULL);
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Esse cumque perferendis dolore rerum atque sit qui et.',NULL);
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Aut minima sint magnam quaerat commodi nemo ducimus.',NULL);
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Non iure tempora est et doloremque accusamus laudantium eum ducimus.',NULL);
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Aut ab omnis quaerat rerum quaerat necessitatibus.',NULL);
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Dolorum ut quisquam doloribus cum dolorum odit iste id.',NULL);
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Eos porro officiis fugiat commodi dolor quidem consectetur velit.',NULL);
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Quis quia officia quae natus alias ea sunt est culpa.',NULL);
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Ipsa consectetur nulla ad necessitatibus ut et facere beatae.',NULL);
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Illo saepe aut cum esse id.',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Ofélia Pereira','Alicia_Souza80@example.com','+552121603579',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Yasmin Reis','Alexandre_Moraes@example.org','+551281949047',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Sophia Franco','Ladislau_Martins76@example.org','+550558525353',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Hélio Barros','Lorenzo30@example.com','+556608896427',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Bernardo Costa','Kleber.Moraes69@example.net','+556864123848',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Silas Pereira','Bruna_Moreira91@example.org','+558835993959',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Gael Franco','Ofelia_Silva@example.com','+556689405542',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Roberto Souza','Sirineu.Nogueira@example.org','+555193348885',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Breno Braga','AnaJulia_Xavier85@example.net','+555338674172',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Deneval Albuquerque','Manuela56@example.org','+555616957629',NULL);
/* -> 20 Insert operations completed in 00:00:03.1379237 taking an average of 00:00:00.1568961 */
/* Committing Transaction */
/* AluguelIdealDatabaseMigration migrated */
/* => 0,3325058s */

