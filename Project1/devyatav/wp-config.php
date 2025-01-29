<?php
/**
 * The base configuration for WordPress
 *
 * The wp-config.php creation script uses this file during the installation.
 * You don't have to use the website, you can copy this file to "wp-config.php"
 * and fill in the values.
 *
 * This file contains the following configurations:
 *
 * * Database settings
 * * Secret keys
 * * Database table prefix
 * * ABSPATH
 *
 * @link https://developer.wordpress.org/advanced-administration/wordpress/wp-config/
 *
 * @package WordPress
 */

// ** Database settings - You can get this info from your web host ** //
/** The name of the database for WordPress */
define( 'DB_NAME', 'wp_dcdae' );

/** Database username */
define( 'DB_USER', 'root' );

/** Database password */
define( 'DB_PASSWORD', '' );

/** Database hostname */
define( 'DB_HOST', 'localhost' );

/** Database charset to use in creating database tables. */
define( 'DB_CHARSET', 'utf8mb4' );

/** The database collate type. Don't change this if in doubt. */
define( 'DB_COLLATE', '' );

/**#@+
 * Authentication unique keys and salts.
 *
 * Change these to different unique phrases! You can generate these using
 * the {@link https://api.wordpress.org/secret-key/1.1/salt/ WordPress.org secret-key service}.
 *
 * You can change these at any point in time to invalidate all existing cookies.
 * This will force all users to have to log in again.
 *
 * @since 2.6.0
 */
define( 'AUTH_KEY',         '&NE:*`fUp_TkkSKu=AQer}7u|nWA1%w6O/pK+2_D:}GW[2j0)=v@0.CyE2R:#czW' );
define( 'SECURE_AUTH_KEY',  '^U5WfYxL^mX9F>iNTo#]u@Wu#g<XCt]T9),ub#IYncB8r.kxR2v&2rKnFY~,1-C-' );
define( 'LOGGED_IN_KEY',    'Te=J990%)<6r_>i[1iQE&=&FYAs#P:TB-[q@-hTZU*&aWs2I;HE>rn>Pi]a-U2/?' );
define( 'NONCE_KEY',        '6Y,CD_ ^WDOMqg_t_^TVk3%du>yzZ@y/{cc<=KKB}9[>^9w|;+CJR(3z:FQwlahc' );
define( 'AUTH_SALT',        'dfV3eSBE|-NZ2p8,5T+cs`4^l`l+f(R]3GRpGSC&<=VB53K/633r|^>g`^4~jTzD' );
define( 'SECURE_AUTH_SALT', '-s,hb.KWm`EuyabGiL`~?V&H]_:Of=`x6c.dhk6;c9gY.N.T%`=1UB#&_2q^Lc7D' );
define( 'LOGGED_IN_SALT',   '=38aqHvUExs:d4E#vaM;O8/qY^g&PKGf+18FNcn5t(UUT[n6wZDo/-C=SY*xBo+I' );
define( 'NONCE_SALT',       'j #jyG#uNd5&dQkOaQ%:BpF1G~p_r^U%5P$IL$rK1vVSR#D}_qES| .vFVbhH?R3' );

/**#@-*/

/**
 * WordPress database table prefix.
 *
 * You can have multiple installations in one database if you give each
 * a unique prefix. Only numbers, letters, and underscores please!
 *
 * At the installation time, database tables are created with the specified prefix.
 * Changing this value after WordPress is installed will make your site think
 * it has not been installed.
 *
 * @link https://developer.wordpress.org/advanced-administration/wordpress/wp-config/#table-prefix
 */
$table_prefix = 'wp_';

/**
 * For developers: WordPress debugging mode.
 *
 * Change this to true to enable the display of notices during development.
 * It is strongly recommended that plugin and theme developers use WP_DEBUG
 * in their development environments.
 *
 * For information on other constants that can be used for debugging,
 * visit the documentation.
 *
 * @link https://developer.wordpress.org/advanced-administration/debug/debug-wordpress/
 */
define( 'WP_DEBUG', false );

/* Add any custom values between this line and the "stop editing" line. */



/* That's all, stop editing! Happy publishing. */

/** Absolute path to the WordPress directory. */
if ( ! defined( 'ABSPATH' ) ) {
	define( 'ABSPATH', __DIR__ . '/' );
}

/** Sets up WordPress vars and included files. */
require_once ABSPATH . 'wp-settings.php';
