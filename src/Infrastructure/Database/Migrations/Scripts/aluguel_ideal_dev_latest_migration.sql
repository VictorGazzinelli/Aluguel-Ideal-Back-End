/* AluguelIdealDatabaseMigration migrating =================================== */

/* Beginning Transaction */
/* ExecuteSqlStatement DROP TABLE IF EXISTS advertisement */
DROP TABLE IF EXISTS advertisement
/* => 0,180033s */

/* ExecuteSqlStatement DROP TABLE IF EXISTS contact */
DROP TABLE IF EXISTS contact
/* => 0,1698387s */

/* CreateTable advertisement */
CREATE TABLE "public"."advertisement" ("id" serial NOT NULL, "title" varchar(255) NOT NULL, "deleted_at" timestamp, CONSTRAINT "PK_advertisement" PRIMARY KEY ("id"));
/* => 0,1954376s */

/* CreateTable contact */
CREATE TABLE "public"."contact" ("id" serial NOT NULL, "name" varchar(255) NOT NULL, "email" varchar(255) NOT NULL, "phone" varchar(255) NOT NULL, "deleted_at" timestamp, CONSTRAINT "PK_contact" PRIMARY KEY ("id"));
/* => 0,177385s */

INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Et atque harum quaerat quia omnis.',NULL);
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Exercitationem et vel reiciendis vero nemo minus consequatur.',NULL);
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Culpa earum ut magnam minima odio deleniti aut repudiandae rerum.',NULL);
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Est officiis eveniet voluptates quia qui sit nobis aut.',NULL);
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Voluptas velit qui occaecati est qui.',NULL);
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Autem tempore dolores.','2020-08-22T15:07:45');
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Minima omnis odit et rem est ab aliquam.',NULL);
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Fugiat nihil sit nemo omnis error numquam pariatur odio.',NULL);
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Tempora ullam at magni est non.',NULL);
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Adipisci quae ab voluptates cumque excepturi distinctio.',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Cecília Barros','Samuel.Saraiva@example.org','+557019556228',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Júlia Braga','Paula63@example.org','+553930050685',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Liz Franco','Tertuliano84@example.org','+550637689034',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('João Lucas Costa','Celia_Santos@example.org','+550348769503',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Isabela Melo','Fabricio.Albuquerque77@example.net','+552157208974',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Liz Albuquerque','JoaoPedro_Moraes10@example.org','+557397894962',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Washington Pereira','Victor_Oliveira75@example.net','+558075158463',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Heloísa Souza','Victor_Santos23@example.org','+556924299920',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Benjamin Oliveira','Melissa56@example.org','+552093359895',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Beatriz Oliveira','Carla.Martins30@example.com','+552691579157',NULL);
/* -> 20 Insert operations completed in 00:00:03.3558642 taking an average of 00:00:00.1677932 */
/* Committing Transaction */
/* AluguelIdealDatabaseMigration migrated */
/* => 0,350776s */

