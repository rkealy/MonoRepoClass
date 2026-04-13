extends Node

var attack_held := false

func _process(delta: float) -> void:
	if Input.is_action_just_pressed("attack"):
		attack_held = true
		print("Attack!!")

	if Input.is_action_just_released("attack"):
		attack_held = false
		print("Attack Released")

	if attack_held:
		print("Attack Held")

	var move := Input.get_vector("move_left", "move_right", "move_up", "move_down")
	if move != Vector2.ZERO:
		print("MOVE ", move)
