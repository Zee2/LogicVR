![Logo](VRLogo1.jpg)

# LogicVR

A work in progress educational logic circuit simulator for UnityXR/Windows Mixed Reality. Initial support will be made for Windows Mixed Reality, with compatibility for SteamVR/Vive/Oculus coming later.

## Features

- Basic logic gates (AND, NOR, NOT, NAND, XOR, etc)
- Basic input components (switches, buttons, toggles, oscillators)
- Basic wiring (editable cables, snapping, busses coming in the future)
- Variable simulation tick-rate (for different hardware capabilities)
- Subassembly creation (designate regions of circuitry as "ICs")
- Immersive input, using full roomscale tracked controllers

## Current status

Backend work is underway, with the node structure of the wiring system nearly complete. Prototype wiring manipulation from tracked controllers is implemented, but it is only a reference implementation. Will extend with standardized "tool" interfacing, to support multitool selection from controllers. Logic simulation backend is functional, with the "tick" engine and basic logic gate functionality supported.
