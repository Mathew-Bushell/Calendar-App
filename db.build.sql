CREATE TABLE `calendardb`.`event` (
  `id-event` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  `description` LONGTEXT NULL,
  `date` DATETIME NOT NULL, 
  `colour` VARCHAR(45) NOT NULL DEFAULT '#4287f5',
  PRIMARY KEY (`id-event`));
