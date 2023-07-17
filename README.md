# SCP_Kick
EXILED plugin to kick members without a specific permission.

## Installation
Download SCP_Kick.dll from [Releases](/Releases).

Move SCP_Kick.dll to .config/EXILED/Plugins and restart server.

## Configuration
Edit values in .config/EXILED/Configs/PORT-config.yml

Example config with default values:
```yml
s_c_p__kick:
# Is the Plugin enabled.
  is_enabled: true
  # Debug mode.
  debug: false
  # Permission required to join.
  permission: 'access.join'
  # Can NorthWood staff bypass this perm?
  n_w_bypass: true
  # Kick message.
  kick_message: 'You don't have the required permissions to join.'
```
* is_enabled
> A boolean; Controls if SCP_Kick is enabled or not.
* debug
> A boolean; Enables some extra logging.
* permission
> A string; Represents an EXILED permission.
* n_w_bypass
> A boolean; Allows NW Staff to bypass the permission check.
* kick_message
> A string; Message displayed on kicked Player's screen.
