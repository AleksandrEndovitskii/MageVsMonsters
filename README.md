# MageVsMonsters

1) There is a mage on the stage (with health, defense and movement speed)
2) The mage must be able to move around the stage, turn (using the arrow keys on the keyboard) and be able to release spells (X button)
3) The mage’s spells must be of several types (with different appearance and damage)
4) The mage must have the ability to change the current spell (Q and W buttons)

1) Monsters must be of several types (with different appearance, amount of health, damage, defense and movement speed)
2) Monsters should be generated randomly behind the stage and sent to the mage
3) There should be no more than 10 monsters on the stage at a time; when one dies, the next one should be born
4) When a spell hits a monster, its health should decrease according to the damage of the spell and the defense of the monster
5) In case of a collision with a mage, his health should decrease according to the mage’s defense and damage from the monster

The size of the stage must be limited.
Various obstacles can be placed on the stage(optional).
Appearance can be indicated by a picture or color.
Damage calculation: health=health-damage*defense(0...1).
