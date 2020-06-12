-- MariaDB dump 10.17  Distrib 10.4.11-MariaDB, for Win64 (AMD64)
--
-- Host: 127.0.0.1    Database: mydb
-- ------------------------------------------------------
-- Server version	10.4.11-MariaDB

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `bill`
--

DROP TABLE IF EXISTS `bill`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `bill` (
  `bill_id` int(11) NOT NULL,
  `total_cost` float NOT NULL,
  `date` datetime NOT NULL,
  `note` varchar(45) DEFAULT NULL,
  `Payment_method_id` int(11) NOT NULL,
  PRIMARY KEY (`bill_id`),
  KEY `fk_Bill_Payment_method1_idx` (`Payment_method_id`),
  CONSTRAINT `fk_Bill_Payment_method1` FOREIGN KEY (`Payment_method_id`) REFERENCES `payment_method` (`Payment_method_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `bill`
--

LOCK TABLES `bill` WRITE;
/*!40000 ALTER TABLE `bill` DISABLE KEYS */;
/*!40000 ALTER TABLE `bill` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `car`
--

DROP TABLE IF EXISTS `car`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `car` (
  `car_id` int(11) NOT NULL,
  `license_plate` varchar(45) DEFAULT NULL,
  `province` varchar(50) DEFAULT NULL,
  `VIN_num` varchar(45) DEFAULT NULL,
  `car_model` varchar(45) NOT NULL,
  `car_brand` varchar(45) NOT NULL,
  `status` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`car_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `car`
--

LOCK TABLES `car` WRITE;
/*!40000 ALTER TABLE `car` DISABLE KEYS */;
/*!40000 ALTER TABLE `car` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `carrental`
--

DROP TABLE IF EXISTS `carrental`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `carrental` (
  `carRental_id` int(11) NOT NULL,
  `rentDate` datetime NOT NULL,
  `returnDate` datetime NOT NULL,
  `customer_id` int(11) NOT NULL,
  `employee_id` int(11) NOT NULL,
  `bill_id` int(11) NOT NULL,
  `car_id` int(11) NOT NULL,
  PRIMARY KEY (`carRental_id`),
  KEY `fk_CarRental_Customer1_idx` (`customer_id`),
  KEY `fk_CarRental_Employee1_idx` (`employee_id`),
  KEY `fk_CarRental_Bill1_idx` (`bill_id`),
  KEY `fk_CarRental_Car1_idx` (`car_id`),
  CONSTRAINT `fk_CarRental_Bill1` FOREIGN KEY (`bill_id`) REFERENCES `bill` (`bill_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_CarRental_Car1` FOREIGN KEY (`car_id`) REFERENCES `car` (`car_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_CarRental_Customer1` FOREIGN KEY (`customer_id`) REFERENCES `customer` (`customer_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_CarRental_Employee1` FOREIGN KEY (`employee_id`) REFERENCES `employee` (`employee_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `carrental`
--

LOCK TABLES `carrental` WRITE;
/*!40000 ALTER TABLE `carrental` DISABLE KEYS */;
/*!40000 ALTER TABLE `carrental` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `customer`
--

DROP TABLE IF EXISTS `customer`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `customer` (
  `customer_id` int(11) NOT NULL,
  `driving_licence_number` varchar(45) NOT NULL,
  `customer_name` varchar(45) NOT NULL,
  `dob` date DEFAULT NULL,
  `address` text DEFAULT NULL,
  `register_date` datetime NOT NULL,
  `phone_number` varchar(45) DEFAULT NULL,
  `job` varchar(45) DEFAULT NULL,
  `id_card_number` varchar(20) NOT NULL,
  `email` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`customer_id`,`driving_licence_number`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `customer`
--

LOCK TABLES `customer` WRITE;
/*!40000 ALTER TABLE `customer` DISABLE KEYS */;
/*!40000 ALTER TABLE `customer` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `employee`
--

DROP TABLE IF EXISTS `employee`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `employee` (
  `employee_id` int(11) NOT NULL,
  `employee_name` varchar(99) NOT NULL,
  `position` varchar(45) DEFAULT NULL,
  `username` varchar(45) DEFAULT NULL,
  `password` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`employee_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='		';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `employee`
--

LOCK TABLES `employee` WRITE;
/*!40000 ALTER TABLE `employee` DISABLE KEYS */;
/*!40000 ALTER TABLE `employee` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `maintenance`
--

DROP TABLE IF EXISTS `maintenance`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `maintenance` (
  `maintenace_id` int(11) NOT NULL,
  `date` date DEFAULT NULL,
  `mileage` varchar(45) DEFAULT NULL,
  `message` varchar(45) DEFAULT NULL,
  `autoparts` varchar(45) DEFAULT NULL,
  `car_id` int(11) NOT NULL,
  PRIMARY KEY (`maintenace_id`),
  KEY `fk_Maintenance_Car1_idx` (`car_id`),
  CONSTRAINT `fk_Maintenance_Car1` FOREIGN KEY (`car_id`) REFERENCES `car` (`car_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `maintenance`
--

LOCK TABLES `maintenance` WRITE;
/*!40000 ALTER TABLE `maintenance` DISABLE KEYS */;
/*!40000 ALTER TABLE `maintenance` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `maintenance_has_repairman`
--

DROP TABLE IF EXISTS `maintenance_has_repairman`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `maintenance_has_repairman` (
  `Maintenance_id` int(11) NOT NULL,
  `Repairman_id` int(11) NOT NULL,
  PRIMARY KEY (`Maintenance_id`,`Repairman_id`),
  KEY `fk_Maintenance_has_Repairman_Repairman1_idx` (`Repairman_id`),
  KEY `fk_Maintenance_has_Repairman_Maintenance1_idx` (`Maintenance_id`),
  CONSTRAINT `fk_Maintenance_has_Repairman_Maintenance1` FOREIGN KEY (`Maintenance_id`) REFERENCES `maintenance` (`maintenace_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_Maintenance_has_Repairman_Repairman1` FOREIGN KEY (`Repairman_id`) REFERENCES `repairman` (`repairman_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `maintenance_has_repairman`
--

LOCK TABLES `maintenance_has_repairman` WRITE;
/*!40000 ALTER TABLE `maintenance_has_repairman` DISABLE KEYS */;
/*!40000 ALTER TABLE `maintenance_has_repairman` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `payment_method`
--

DROP TABLE IF EXISTS `payment_method`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `payment_method` (
  `Payment_method_id` int(11) NOT NULL,
  `medthod_name` varchar(45) NOT NULL,
  `detail` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`Payment_method_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `payment_method`
--

LOCK TABLES `payment_method` WRITE;
/*!40000 ALTER TABLE `payment_method` DISABLE KEYS */;
/*!40000 ALTER TABLE `payment_method` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `repairman`
--

DROP TABLE IF EXISTS `repairman`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `repairman` (
  `repairman_id` int(11) NOT NULL,
  `repairman_name` varchar(99) NOT NULL,
  `expertise` varchar(45) DEFAULT NULL,
  `username` varchar(45) DEFAULT NULL,
  `password` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`repairman_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `repairman`
--

LOCK TABLES `repairman` WRITE;
/*!40000 ALTER TABLE `repairman` DISABLE KEYS */;
/*!40000 ALTER TABLE `repairman` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-05-30 23:11:55
