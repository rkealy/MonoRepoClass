extends Node

@export var receiver: Receiver

func _ready():
	print("Hello Friend")
	receiver.OnCalled()
