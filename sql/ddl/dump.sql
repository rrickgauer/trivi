-- MySQL dump 10.13  Distrib 8.0.38, for Win64 (x86_64)
--
-- Host: 104.225.208.163    Database: Trivi_Dev
-- ------------------------------------------------------
-- Server version	8.0.37-0ubuntu0.20.04.3

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Current Database: Trivi_Dev
--

USE Trivi_Dev;

--
-- Table structure for table `Answers_MC`
--

DROP TABLE IF EXISTS Answers_MC;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE Answers_MC (
  internal_id int unsigned NOT NULL AUTO_INCREMENT /*!80023 INVISIBLE */,
  id varchar(36) NOT NULL,
  question_id varchar(36) NOT NULL,
  answer varchar(40) DEFAULT NULL,
  is_correct tinyint(1) NOT NULL DEFAULT '0',
  created_on timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (internal_id),
  UNIQUE KEY internal_id (internal_id),
  UNIQUE KEY id (id),
  KEY question_id (question_id),
  CONSTRAINT Answers_MC_ibfk_1 FOREIGN KEY (question_id) REFERENCES Questions_MC (id) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Collections`
--

DROP TABLE IF EXISTS Collections;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE Collections (
  internal_id int unsigned NOT NULL AUTO_INCREMENT /*!80023 INVISIBLE */,
  id char(36) NOT NULL,
  `name` varchar(50) NOT NULL,
  user_id char(36) NOT NULL,
  created_on timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (internal_id),
  UNIQUE KEY internal_id (internal_id),
  UNIQUE KEY id (id),
  KEY user_id (user_id),
  CONSTRAINT Collections_ibfk_1 FOREIGN KEY (user_id) REFERENCES Users (id) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=38 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Error_Message_Groups`
--

DROP TABLE IF EXISTS Error_Message_Groups;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE Error_Message_Groups (
  id smallint unsigned NOT NULL,
  `name` varchar(30) NOT NULL,
  PRIMARY KEY (id),
  UNIQUE KEY id (id),
  UNIQUE KEY `name` (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Error_Messages`
--

DROP TABLE IF EXISTS Error_Messages;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE Error_Messages (
  id bigint unsigned NOT NULL,
  group_id smallint unsigned NOT NULL,
  message varchar(250) NOT NULL,
  PRIMARY KEY (id),
  UNIQUE KEY id (id),
  KEY group_id (group_id),
  CONSTRAINT Error_Messages_ibfk_1 FOREIGN KEY (group_id) REFERENCES Error_Message_Groups (id) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Question_Types`
--

DROP TABLE IF EXISTS Question_Types;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE Question_Types (
  id smallint unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(30) NOT NULL,
  prefix char(2) NOT NULL,
  PRIMARY KEY (id),
  UNIQUE KEY id (id),
  UNIQUE KEY `name` (`name`),
  UNIQUE KEY prefix (prefix)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Questions`
--

DROP TABLE IF EXISTS Questions;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE Questions (
  internal_id int unsigned NOT NULL AUTO_INCREMENT /*!80023 INVISIBLE */,
  id varchar(36) NOT NULL,
  collection_id char(36) NOT NULL,
  question_type_id smallint unsigned NOT NULL,
  prompt varchar(40) NOT NULL,
  created_on timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (internal_id),
  UNIQUE KEY internal_id (internal_id),
  UNIQUE KEY id (id),
  KEY collection_id (collection_id),
  KEY question_type_id (question_type_id),
  CONSTRAINT Questions_ibfk_1 FOREIGN KEY (collection_id) REFERENCES Collections (id) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT Questions_ibfk_2 FOREIGN KEY (question_type_id) REFERENCES Question_Types (id) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=93 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Questions_MC`
--

DROP TABLE IF EXISTS Questions_MC;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE Questions_MC (
  internal_id int unsigned NOT NULL AUTO_INCREMENT /*!80023 INVISIBLE */,
  id varchar(36) NOT NULL,
  PRIMARY KEY (internal_id),
  UNIQUE KEY internal_id (internal_id),
  UNIQUE KEY id (id),
  CONSTRAINT Questions_MC_ibfk_1 FOREIGN KEY (id) REFERENCES Questions (id) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=29 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Questions_SA`
--

DROP TABLE IF EXISTS Questions_SA;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE Questions_SA (
  internal_id int unsigned NOT NULL AUTO_INCREMENT /*!80023 INVISIBLE */,
  id varchar(36) NOT NULL,
  correct_answer varchar(40) NOT NULL,
  PRIMARY KEY (internal_id),
  UNIQUE KEY internal_id (internal_id),
  UNIQUE KEY id (id),
  CONSTRAINT Questions_SA_ibfk_1 FOREIGN KEY (id) REFERENCES Questions (id) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=34 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Questions_TF`
--

DROP TABLE IF EXISTS Questions_TF;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE Questions_TF (
  internal_id int unsigned NOT NULL AUTO_INCREMENT /*!80023 INVISIBLE */,
  id varchar(36) NOT NULL,
  correct_answer tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (internal_id),
  UNIQUE KEY internal_id (internal_id),
  UNIQUE KEY id (id),
  CONSTRAINT Questions_TF_ibfk_1 FOREIGN KEY (id) REFERENCES Questions (id) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=28 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Users`
--

DROP TABLE IF EXISTS Users;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE Users (
  internal_id int unsigned NOT NULL AUTO_INCREMENT /*!80023 INVISIBLE */,
  id char(36) NOT NULL,
  email varchar(200) NOT NULL,
  `password` varchar(250) NOT NULL,
  created_on timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (internal_id),
  UNIQUE KEY internal_id (internal_id),
  UNIQUE KEY id (id),
  UNIQUE KEY email (email)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Temporary view structure for view `View_Answers_MC`
--

DROP TABLE IF EXISTS View_Answers_MC;
/*!50001 DROP VIEW IF EXISTS View_Answers_MC*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `View_Answers_MC` AS SELECT 
 1 AS answer_id,
 1 AS answer_question_id,
 1 AS answer_answer,
 1 AS answer_is_correct,
 1 AS answer_created_on,
 1 AS answer_question_collection_id,
 1 AS answer_question_collection_user_id*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `View_Collections`
--

DROP TABLE IF EXISTS View_Collections;
/*!50001 DROP VIEW IF EXISTS View_Collections*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `View_Collections` AS SELECT 
 1 AS collection_id,
 1 AS collection_name,
 1 AS collection_user_id,
 1 AS collection_created_on*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `View_Error_Messages`
--

DROP TABLE IF EXISTS View_Error_Messages;
/*!50001 DROP VIEW IF EXISTS View_Error_Messages*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `View_Error_Messages` AS SELECT 
 1 AS error_id,
 1 AS group_name,
 1 AS error_group_id,
 1 AS error_message*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `View_Questions`
--

DROP TABLE IF EXISTS View_Questions;
/*!50001 DROP VIEW IF EXISTS View_Questions*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `View_Questions` AS SELECT 
 1 AS question_id,
 1 AS question_collection_id,
 1 AS question_question_type_id,
 1 AS question_prompt,
 1 AS question_created_on,
 1 AS collection_user_id*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `View_Questions_MC`
--

DROP TABLE IF EXISTS View_Questions_MC;
/*!50001 DROP VIEW IF EXISTS View_Questions_MC*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `View_Questions_MC` AS SELECT 
 1 AS question_id,
 1 AS question_collection_id,
 1 AS question_question_type_id,
 1 AS question_prompt,
 1 AS question_created_on,
 1 AS collection_user_id*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `View_Questions_SA`
--

DROP TABLE IF EXISTS View_Questions_SA;
/*!50001 DROP VIEW IF EXISTS View_Questions_SA*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `View_Questions_SA` AS SELECT 
 1 AS question_id,
 1 AS question_collection_id,
 1 AS question_question_type_id,
 1 AS question_prompt,
 1 AS question_created_on,
 1 AS collection_user_id,
 1 AS question_correct_answer*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `View_Questions_TF`
--

DROP TABLE IF EXISTS View_Questions_TF;
/*!50001 DROP VIEW IF EXISTS View_Questions_TF*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `View_Questions_TF` AS SELECT 
 1 AS question_id,
 1 AS question_collection_id,
 1 AS question_question_type_id,
 1 AS question_prompt,
 1 AS question_created_on,
 1 AS collection_user_id,
 1 AS question_correct_answer*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `View_Users`
--

DROP TABLE IF EXISTS View_Users;
/*!50001 DROP VIEW IF EXISTS View_Users*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `View_Users` AS SELECT 
 1 AS user_id,
 1 AS user_email,
 1 AS user_password,
 1 AS user_created_on*/;
SET character_set_client = @saved_cs_client;

--
-- Dumping events for database 'Trivi_Dev'
--

--
-- Dumping routines for database 'Trivi_Dev'
--

--
-- Current Database: Trivi_Dev
--

USE Trivi_Dev;

--
-- Final view structure for view `View_Answers_MC`
--

/*!50001 DROP VIEW IF EXISTS View_Answers_MC*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=main@`%` SQL SECURITY DEFINER */
/*!50001 VIEW View_Answers_MC AS select a.id AS answer_id,a.question_id AS answer_question_id,a.answer AS answer_answer,a.is_correct AS answer_is_correct,a.created_on AS answer_created_on,q.question_collection_id AS answer_question_collection_id,q.collection_user_id AS answer_question_collection_user_id from (Answers_MC a join View_Questions_MC q on((q.question_id = a.question_id))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `View_Collections`
--

/*!50001 DROP VIEW IF EXISTS View_Collections*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=main@`%` SQL SECURITY DEFINER */
/*!50001 VIEW View_Collections AS select c.id AS collection_id,c.`name` AS collection_name,c.user_id AS collection_user_id,c.created_on AS collection_created_on from Collections c */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `View_Error_Messages`
--

/*!50001 DROP VIEW IF EXISTS View_Error_Messages*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=main@`%` SQL SECURITY DEFINER */
/*!50001 VIEW View_Error_Messages AS select e.id AS error_id,g.`name` AS group_name,e.group_id AS error_group_id,e.message AS error_message from (Error_Messages e left join Error_Message_Groups g on((g.id = e.group_id))) order by e.id */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `View_Questions`
--

/*!50001 DROP VIEW IF EXISTS View_Questions*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=main@`%` SQL SECURITY DEFINER */
/*!50001 VIEW View_Questions AS select q.id AS question_id,c.id AS question_collection_id,q.question_type_id AS question_question_type_id,q.prompt AS question_prompt,q.created_on AS question_created_on,c.user_id AS collection_user_id from (Questions q join Collections c on((c.id = q.collection_id))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `View_Questions_MC`
--

/*!50001 DROP VIEW IF EXISTS View_Questions_MC*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=main@`%` SQL SECURITY DEFINER */
/*!50001 VIEW View_Questions_MC AS select q.question_id AS question_id,q.question_collection_id AS question_collection_id,q.question_question_type_id AS question_question_type_id,q.question_prompt AS question_prompt,q.question_created_on AS question_created_on,q.collection_user_id AS collection_user_id from View_Questions q */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `View_Questions_SA`
--

/*!50001 DROP VIEW IF EXISTS View_Questions_SA*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=main@`%` SQL SECURITY DEFINER */
/*!50001 VIEW View_Questions_SA AS select q.question_id AS question_id,q.question_collection_id AS question_collection_id,q.question_question_type_id AS question_question_type_id,q.question_prompt AS question_prompt,q.question_created_on AS question_created_on,q.collection_user_id AS collection_user_id,sa.correct_answer AS question_correct_answer from (View_Questions q join Questions_SA sa on((sa.id = q.question_id))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `View_Questions_TF`
--

/*!50001 DROP VIEW IF EXISTS View_Questions_TF*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=main@`%` SQL SECURITY DEFINER */
/*!50001 VIEW View_Questions_TF AS select q.question_id AS question_id,q.question_collection_id AS question_collection_id,q.question_question_type_id AS question_question_type_id,q.question_prompt AS question_prompt,q.question_created_on AS question_created_on,q.collection_user_id AS collection_user_id,tf.correct_answer AS question_correct_answer from (View_Questions q join Questions_TF tf on((tf.id = q.question_id))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `View_Users`
--

/*!50001 DROP VIEW IF EXISTS View_Users*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=main@`%` SQL SECURITY DEFINER */
/*!50001 VIEW View_Users AS select u.id AS user_id,u.email AS user_email,u.`password` AS user_password,u.created_on AS user_created_on from Users u */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-07-17 15:21:09
-- MySQL dump 10.13  Distrib 8.0.38, for Win64 (x86_64)
--
-- Host: 104.225.208.163    Database: Trivi_Dev
-- ------------------------------------------------------
-- Server version	8.0.37-0ubuntu0.20.04.3

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Dumping data for table `Error_Message_Groups`
--
-- ORDER BY:  id

LOCK TABLES Error_Message_Groups WRITE;
/*!40000 ALTER TABLE Error_Message_Groups DISABLE KEYS */;
REPLACE INTO Error_Message_Groups VALUES (1,'Misc'),(2,'Authorization'),(3,'Answers');
/*!40000 ALTER TABLE Error_Message_Groups ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `Error_Messages`
--
-- ORDER BY:  id

LOCK TABLES Error_Messages WRITE;
/*!40000 ALTER TABLE Error_Messages DISABLE KEYS */;
REPLACE INTO Error_Messages VALUES (200,2,'Invalid email or password.'),(201,2,'The email you have provided is already associated with an account.'),(202,2,'The passwords do not match.'),(203,2,'Please lengthen the password to 8 or more characters.'),(300,3,'Invalid ID format');
/*!40000 ALTER TABLE Error_Messages ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `Question_Types`
--
-- ORDER BY:  id

LOCK TABLES Question_Types WRITE;
/*!40000 ALTER TABLE Question_Types DISABLE KEYS */;
REPLACE INTO Question_Types VALUES (1,'Multiple Choice','mc'),(2,'True False','tf'),(3,'Short Answer','sa');
/*!40000 ALTER TABLE Question_Types ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-07-17 15:21:13
