extends Area3D

@export var rotation_speed: float = 180.0  # Spin speed (degrees per second)

func _ready() -> void:
	body_entered.connect(_on_body_entered)  # Run function when something enters

func _process(delta: float) -> void:
	# Rotate smoothly every frame (frame-rate independent)
	rotate_y(deg_to_rad(rotation_speed * delta))

func _on_body_entered(body: Node) -> void:
	if not body.is_in_group("Player"):
		return  # Ignore non-player objects
	
	print("Coin collected!")
	queue_free()  # Remove the coin
